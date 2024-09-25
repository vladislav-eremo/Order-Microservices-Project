using Microsoft.EntityFrameworkCore;

namespace OrderService.Domain.Entities
{
    [Index("Id")]
    [Index("Name")]
    public class ProductEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public List<OrderEntity> Orders { get; set; } = [];
    }
}
