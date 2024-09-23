using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.DTOs
{
    public class CreateOrderDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int[] ProductIds { get; set; }
    }
}
