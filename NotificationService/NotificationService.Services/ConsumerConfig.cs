using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Services
{
    public class ConsumerConfig
    {
        public string HostName { get; set; } = "localhost";
        public string Exchange { get; set; } = "notification-service-exchange";
        public string QueueName { get; set; } = "notification-service-queue";
    }
}
