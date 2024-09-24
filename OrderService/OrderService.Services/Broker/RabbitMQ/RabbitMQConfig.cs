
namespace OrderService.Services.Broker.RabbitMQ
{
    public class RabbitMQConfig
    {
        public string OrderAcceptedExchange { get; set; } = "OrderService-Order-Accepted";
        public string Hostname { get; set; } =  "localhost";
    }
}
