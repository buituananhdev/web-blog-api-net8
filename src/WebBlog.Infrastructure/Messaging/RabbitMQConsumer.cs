using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace WebBlog.Infrastructure.Messaging
{
    public class RabbitMQConsumer
    {
        private readonly RabbitMQClient _rabbitMQClient;

        public RabbitMQConsumer(RabbitMQClient rabbitMQClient)
        {
            _rabbitMQClient = rabbitMQClient;
        }

        public void Consume(string queueName, Action<string> processMessage)
        {
            var channel = _rabbitMQClient.GetChannel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                processMessage(message);
                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);
        }
    }
}
