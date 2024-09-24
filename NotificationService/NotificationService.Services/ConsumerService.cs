using Microsoft.Extensions.Configuration;
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
        private ConsumerConfig _consumerConfig;

        private static ConsumerConfig BuildConfig(IConfiguration configuration)
        {
            var defaultConfig = new ConsumerConfig();

            return new ConsumerConfig
            {
                HostName = configuration["NOTIF_S_CONSUMER_HOSTNAME"] ?? defaultConfig.HostName,
                Exchange = configuration["NOTIF_S_CONSUMER_EXCHANGE"] ?? defaultConfig.Exchange,
                QueueName = configuration["NOTIF_S_CONSUMER_QUEUE"] ?? defaultConfig.QueueName
            };
        }

        public ConsumerService(ILogger<ConsumerService> logger, IConfiguration config)
        {
            _consumerConfig = BuildConfig(config);

            logger.LogInformation("Initializing consumer service with host: {hostname}", _consumerConfig.HostName);

            var factory = new ConnectionFactory { 
                 HostName = _consumerConfig.HostName
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var orderData = JsonSerializer.Deserialize<CreateOrderDto>(message);
                logger.LogInformation($"Order received");
                if (orderData != null ) logger.LogInformation("Order data: {Description}", orderData.Description);
            };

            channel.QueueBind(_consumerConfig.QueueName, exchange: _consumerConfig.Exchange, routingKey: string.Empty);

            channel.BasicConsume(_consumerConfig.QueueName, autoAck: true, consumer: consumer);

            logger.LogInformation("Consumer service ready");
        }
    }
}
