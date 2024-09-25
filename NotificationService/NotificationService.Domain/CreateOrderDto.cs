using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Domain
{
    public class CreateOrderDto
    {
        public required string Address { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerPhoneNumber { get; set; }
        public required string Description { get; set; }
        public required int[] ProductIds { get; set; }

        public override string ToString()
        {
            return $"Order - {Description} | Customer - {CustomerName} | Address - {CustomerPhoneNumber}";
        }
    }
}
