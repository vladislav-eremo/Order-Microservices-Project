using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.DTOs
{
    public record GetOrderDto
    {
        public int Id { get; set; }
        public required string Address { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerPhoneNumber { get; set; }
        public required string Description { get; set; }
        public required int[] ProductIds { get; set; }
    }
}
