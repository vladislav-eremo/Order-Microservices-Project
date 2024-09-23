using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure
{
    public class OrderServiceDBContext : DbContext
    {
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<OrderProductEntity> OrderProducts => Set<OrderProductEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("OrderServiceDB");
            optionsBuilder.LogTo(message => Console.WriteLine(message));
        }
    }
}
