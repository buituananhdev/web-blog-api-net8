using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebBlog.Application.Dtos;
using WebBlog.Application.ExternalServices;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebBlog.Infrastructure.Messaging;

namespace WebBlog.Infrastructure.Workers
{
    public class EmailWorker : BackgroundService
    {
        private readonly RabbitMQConsumer _consumer;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public EmailWorker(RabbitMQConsumer consumer, IEmailService emailService, IConfiguration configuration)
        {
            _consumer = consumer;
            _emailService = emailService;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Consume(_configuration["RabbitMQ:QueueName"], async message =>
            {
                // Deserialize the message
                MailDataDto emailMessage = JsonConvert.DeserializeObject<MailDataDto>(message);
                await _emailService.SendMailAsync(emailMessage);
            });
        }
    }
}
