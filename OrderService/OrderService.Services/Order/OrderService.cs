using AutoMapper;
using Microsoft.Extensions.Logging;
using OrderService.Domain.DTOs;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Repositories.Order;
using OrderService.Services.Broker;
using System.Text.Json;

namespace OrderService.Services.Order
{
    public class OrderService(
        IOrderRepository orderRepository, 
        IBroker brokerService, 
        IMapper mapper, 
        ILogger<OrderService> logger) : IOrderService
    { 
        public void CreateOrder(CreateOrderDto order)
        {
            logger.LogInformation("Creating new order {order}", order);

            orderRepository.Add(mapper.Map<OrderEntity>(order));
            orderRepository.Save();

            try
            {
                logger.LogInformation("Publishing order {order} to other services", order);
                brokerService.SendMessage(JsonSerializer.Serialize(order));
            }
            catch (Exception e)
            {
                logger.LogError("Error on publishing order {order}", order);
                logger.LogError("Caugt error {error}", e);
                throw;
            }
        }

        public List<GetOrderDto> GetAllOrders()
        {
            return mapper.Map<List<GetOrderDto>>(orderRepository.Get());
        }
    }
}
