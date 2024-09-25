using AutoMapper;
using Microsoft.Extensions.Logging;
using OrderService.Domain.DTOs;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Repositories;
using OrderService.Services.Broker;
using System.Text.Json;

namespace OrderService.Services.Order
{
    public class OrderService(
        IGenericRepository<OrderEntity> orderRepository,
        IGenericRepository<OrderProductEntity> orderProduct,
        IBroker brokerService, 
        IMapper mapper, 
        ILogger<OrderService> logger) : IOrderService
    { 
        public void CreateOrder(CreateOrderDto orderData)
        {
            logger.LogInformation("Creating new order {order}", orderData);

            var orderEntity = mapper.Map<OrderEntity>(orderData);

            orderRepository.Add(orderEntity);
            orderRepository.Save();

            foreach (var product in orderData.ProductIds
                                                    .GroupBy(id => id)
                                                    .Select(x => new {Id = x.Key, Count = x.Count()})) {
                orderProduct.Add(new OrderProductEntity
                {
                    OrderId = orderEntity.Id,
                    ProductId = product.Id,
                    Count = product.Count
                });
            }

            orderProduct.Save();

            try
            {
                logger.LogInformation("Publishing order {order} to other services", orderData);
                brokerService.SendMessage(JsonSerializer.Serialize(orderData));
            }
            catch (Exception e)
            {
                logger.LogError("Error on publishing order {order}", orderData);
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
