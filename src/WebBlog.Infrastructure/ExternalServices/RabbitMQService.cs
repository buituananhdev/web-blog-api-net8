using Newtonsoft.Json;
using WebBlog.Application.Dtos;
using WebBlog.Application.ExternalServices;

namespace WebBlog.Infrastructure.Messaging
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly RabbitMQPublisher _publisher;

        public RabbitMQService(RabbitMQPublisher publisher)
        {
            _publisher = publisher;
        }

        public void SendEmailAsync(MailDataDto mailData)
        {
            var jsonMessage = JsonConvert.SerializeObject(mailData);
            _publisher.Publish("email_queue", jsonMessage);
        }
    }
}
