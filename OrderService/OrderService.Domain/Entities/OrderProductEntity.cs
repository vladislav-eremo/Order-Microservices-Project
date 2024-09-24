
using Microsoft.EntityFrameworkCore;

namespace OrderService.Domain.Entities
{
    [Index("OrderId")]
    [Index("ProductId")]
    public class OrderProductEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
