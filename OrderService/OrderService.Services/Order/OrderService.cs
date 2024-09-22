using AutoMapper;
using OrderService.Domain.DTOs;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Repositories.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Services.Order
{
    public class OrderService(IOrderRepository orderRepository, IMapper mapper) : IOrderService
    { 
        public void CreateOrder(OrderDto order)
        {
            orderRepository.Add(mapper.Map<OrderEntity>(order));
        }

        public List<OrderDto> GetAllOrders()
        {
            return mapper.Map<List<OrderDto>>(orderRepository.Get());
        }
    }
}
