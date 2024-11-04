﻿using Microsoft.Extensions.DependencyInjection;
using WebBlog.Application.ExternalServices;
using WebBlog.Application.Repositories;
using WebBlog.Infrastructure.ExternalServices;
using WebBlog.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WebBlog.Application.Services.Cache;
using WebBlog.Infrastructure.Cache;

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
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                var connectionString = configuration.GetConnectionString("Redis")
                ?? throw new InvalidOperationException("Connection string 'Redis' not found.");
                redisOptions.Configuration = connectionString;
            });

            return services;
        }
    }
}