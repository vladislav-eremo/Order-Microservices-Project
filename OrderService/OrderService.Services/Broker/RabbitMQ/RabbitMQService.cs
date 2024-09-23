using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Services.Broker.RabbitMQ
{
    public class RabbitMQService : IBroker
    {
        private readonly ConnectionFactory _connectionFactory;
        public RabbitMQService() {
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(RabbitMQConfig.RabbitMQUri)
            };
        }
        public void SendMessage(string message)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(RabbitMQConfig.OrderAcceptedExchange, ExchangeType.Fanout);

            channel.BasicPublish(
                exchange: RabbitMQConfig.OrderAcceptedExchange,
                routingKey: "",
                body: Encoding.UTF8.GetBytes(message));
        }
    }
}
