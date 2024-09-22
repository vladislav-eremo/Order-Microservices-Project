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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderEntity>().HasData(
                new OrderEntity { Name = "Заказ на создание макета", Description = "Создать макет продуктового сайта" },
                new OrderEntity { Name = "Заказ на создание API сервиса анализа данных", Description = "Создать API" });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("OrderServiceDB");
        }
    }
}
