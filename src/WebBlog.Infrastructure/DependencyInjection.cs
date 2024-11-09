using Microsoft.Extensions.DependencyInjection;
using WebBlog.Application.ExternalServices;
using WebBlog.Application.Repositories;
using WebBlog.Infrastructure.ExternalServices;
using WebBlog.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WebBlog.Application.Services.Cache;
using WebBlog.Infrastructure.Cache;
using WebBlog.Infrastructure.Messaging;
using WebBlog.Infrastructure.Workers;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using Microsoft.Extensions.Hosting;
using WebBlog.Application.Services.Logging;
using WebBlog.Infrastructure.Logging;

namespace WebBlog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("MySQL")
                ?? throw new InvalidOperationException("Connection string 'MySQL' not found.");

                // Specify the MySQL Server Version explicitly
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddSingleton<RabbitMQClient>(provider =>
            {
                var hostname = configuration["RabbitMQ:HostName"]
                               ?? throw new InvalidOperationException("RabbitMQ hostname not found.");
                var username = configuration["RabbitMQ:Username"]
                               ?? throw new InvalidOperationException("RabbitMQ username not found.");
                var password = configuration["RabbitMQ:Password"]
                               ?? throw new InvalidOperationException("RabbitMQ password not found.");

                return new RabbitMQClient(hostname, username, password);
            });
            services.AddSingleton<RabbitMQConsumer>();
            services.AddSingleton<RabbitMQPublisher>();
            services.AddSingleton<IRabbitMQService, RabbitMQService>();
            services.AddHostedService<EmailWorker>();
            services.AddSingleton<ILoggerService, SerilogLogger>();
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                var connectionString = configuration.GetConnectionString("Redis")
                ?? throw new InvalidOperationException("Connection string 'Redis' not found.");
                redisOptions.Configuration = connectionString;
            });

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = $"{configuration["ElasticSearch:DefaultIndex"]}-{DateTime.UtcNow:yyyy.MM.dd}",
                })
                .CreateLogger();

            return services;
        }

        public static IHostBuilder UseSerilogLogging(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog();
            return hostBuilder;
        }
    }
}
