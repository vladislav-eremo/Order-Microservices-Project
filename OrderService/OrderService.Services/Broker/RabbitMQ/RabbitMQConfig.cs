
namespace OrderService.Services.Broker.RabbitMQ
{
    public class RabbitMQConfig
    {
        public const string OrderAcceptedExchange = "OrderService-Order-Accepted";
        public const string RabbitMQUri = "amqp://localhost:6001";
    }
}
