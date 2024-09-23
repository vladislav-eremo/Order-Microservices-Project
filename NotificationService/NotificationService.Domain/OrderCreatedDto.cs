namespace NotificationService.Domain
{
    public class OrderCreatedDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public List<ProductDto> Products { get; set; } = [];
    }
}
