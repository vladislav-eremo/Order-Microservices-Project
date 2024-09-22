using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories.Order
{
    public class OrderRepository(OrderServiceDBContext db) : IOrderRepository
    {
        public IQueryable<OrderEntity> Get()
        {
            return db.Orders.AsQueryable().AsNoTracking();
        }
        public void Add(OrderEntity entity)
        {
            db.Orders.Add(entity);
        }
        public void Update(OrderEntity entity)
        {
            db.Orders.Update(entity);
        }
        public void Delete(OrderEntity entity)
        {
            db.Remove(entity);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
