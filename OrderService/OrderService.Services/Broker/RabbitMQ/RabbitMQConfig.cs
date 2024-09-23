using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Services.Broker.RabbitMQ
{
    public class RabbitMQConfig
    {
        public const string OrderAcceptedExchange = "OrderService-Order-Accepted";
        public const string RabbitMQUri = "amqp://localhost:63291";
    }
}
