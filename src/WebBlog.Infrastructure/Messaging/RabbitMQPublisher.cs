using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

namespace WebBlog.Infrastructure.Messaging
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClient _rabbitMQClient;

        public RabbitMQPublisher(RabbitMQClient rabbitMQClient)
        {
            _rabbitMQClient = rabbitMQClient;
            
        }

        public void Publish(string queueName, string message)
        {
            var channel = _rabbitMQClient.GetChannel();
            channel.QueueDeclare(queue: "email_queue",
                             durable: true,  // Tạo queue bền vững
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
