using OrderService.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Services.Order
{
    public interface IOrderService
    {
        void CreateOrder(CreateOrderDto order);
        List<GetOrderDto> GetAllOrders();
    }
}
