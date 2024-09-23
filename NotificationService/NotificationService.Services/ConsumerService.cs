using Microsoft.Extensions.Logging;
using NotificationService.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NotificationService.Services
{
    public class ConsumerService
    {

        private const string queueName = "order-consumer-service";
        public ConsumerService(ILogger<ConsumerService> logger)
        {
            logger.LogInformation("Initializing consumer service...");

            var factory = new ConnectionFactory { 
                Uri = new Uri("amqp://localhost:6001") 
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var orderData = JsonSerializer.Deserialize<OrderCreatedDto>(message);
                logger.LogInformation($"Получено сообщение: {message}");
                if (orderData != null ) logger.LogInformation($"Заказ получен: {orderData.Name}");
            };

            channel.QueueBind(queueName, exchange: "OrderService-Order-Accepted", routingKey: string.Empty);

            channel.BasicConsume(queueName, autoAck: true, consumer: consumer);

            logger.LogInformation("Consumer service ready");
        }
    }
}
