using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories.Order
{
    public interface IOrderRepository
    {
        IQueryable<OrderEntity> Get();
        void Add(OrderEntity entity);
        void Update(OrderEntity entity);
        void Delete(OrderEntity entity);
        void Save();
    }
}
