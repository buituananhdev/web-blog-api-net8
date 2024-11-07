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

        public EmailWorker(RabbitMQConsumer consumer, IEmailService emailService)
        {
            _consumer = consumer;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Consume("email_queue", async message =>
            {
                // Deserialize the message
                MailDataDto emailMessage = JsonConvert.DeserializeObject<MailDataDto>(message);
                await _emailService.SendMailAsync(emailMessage);
            });
        }
    }
}
