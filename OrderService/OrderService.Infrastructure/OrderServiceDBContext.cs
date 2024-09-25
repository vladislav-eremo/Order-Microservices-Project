using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure
{
    public class OrderServiceDBContext : DbContext
    {
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<OrderProductEntity> OrderProducts => Set<OrderProductEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderEntity>()
                    .HasIndex(e => e.Id).IsUnique();

            modelBuilder.Entity<OrderEntity>()
                    .HasIndex(e => e.CustomerPhoneNumber);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("OrderServiceDB");
            optionsBuilder.LogTo(message => Console.WriteLine(message));
        }
    }
}
