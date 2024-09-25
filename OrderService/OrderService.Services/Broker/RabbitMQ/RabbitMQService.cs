using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;

namespace OrderService.Services.Broker.RabbitMQ
{
    public class RabbitMQService : IBroker
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly RabbitMQConfig _rabbitMQConfig;

        public RabbitMQService(ILogger<RabbitMQService> logger, IConfiguration config) {
            var defaultConfig = new RabbitMQConfig();

            _rabbitMQConfig = new RabbitMQConfig
            {
                Hostname = config["ORDER_S_RABBITMQ_HOSTNAME"] ?? defaultConfig.Hostname,
                OrderAcceptedExchange = config["ORDER_S_RABBITMQ_ORDER_ACCEPTED_EXCHANGE"] ?? defaultConfig.OrderAcceptedExchange
            };

            _connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitMQConfig.Hostname,
            };

            logger.LogInformation("Configured RABBITMQ connection with host: {Hostname}", _rabbitMQConfig.Hostname);
        }

        public void SendMessage(string message)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(_rabbitMQConfig.OrderAcceptedExchange, ExchangeType.Fanout);

            channel.BasicPublish(
                exchange: _rabbitMQConfig.OrderAcceptedExchange,
                routingKey: string.Empty,
                body: Encoding.UTF8.GetBytes(message));
        }
    }
}
