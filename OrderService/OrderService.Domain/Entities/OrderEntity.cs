using Microsoft.EntityFrameworkCore;

namespace OrderService.Domain.Entities
{
    [Index("Id")]
    [Index("CustomerPhoneNumber", IsUnique = true)]
    [Index("OrderDate")]
    public class OrderEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public required string Address { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerPhoneNumber { get; set; }
        public required string Description { get; set; }
        public List<ProductEntity> Products { get; set; } = [];
    }
}
