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
        private readonly ILogger<ConsumerService> _logger;
        private readonly ConsumerConfig _consumerConfig;
        private static IConnection? _connection;
        private static IModel? _channel;
        private static EventingBasicConsumer? _consumer;

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

        private void InstantiateConnection(ConnectionFactory factory, int retryTimes = 5, int Interval = 1000)
        {
            _logger.LogInformation("Starting connection instantiate...");

            var attempts = retryTimes;

            for(int i = 0; i < attempts; i++)
            {
                try
                {
                    _logger.LogInformation("#{i} Connecting attempt", i + 1);
                    _connection = factory.CreateConnection();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Connection attempt failed");
                    _logger.LogError("Error on connectiong {ex}", ex);

                    Thread.Sleep(Interval);
                }
            }

        } 

        public ConsumerService(ILogger<ConsumerService> logger, IConfiguration config)
        {
            _logger = logger;
            _consumerConfig = BuildConfig(config);

            logger.LogInformation("Initializing consumer service with host: {hostname}", _consumerConfig.HostName);

            var factory = new ConnectionFactory { 
                 HostName = _consumerConfig.HostName
            };

            InstantiateConnection(factory);

            if (_connection == null) throw new Exception("Unable to create channel for consumer");

            _channel = _connection.CreateModel();

            _consumer = new EventingBasicConsumer(_channel);

            _consumer.Received += (model, ea) =>
            {
                logger.LogInformation($"Order received");
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var orderData = JsonSerializer.Deserialize<CreateOrderDto>(message);
                if (orderData != null ) logger.LogInformation("Order data: {orderData}", orderData);
            };

            _channel.ExchangeDeclare(_consumerConfig.Exchange, "fanout");

            _channel.QueueDeclare(_consumerConfig.QueueName, false, false, false);

            _channel.QueueBind(_consumerConfig.QueueName, exchange: _consumerConfig.Exchange, routingKey: string.Empty);

            _channel.BasicConsume(_consumerConfig.QueueName, autoAck: true, consumer: _consumer);

            logger.LogInformation("Consumer service ready");
        }
    }
}
