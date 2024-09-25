using OrderService.Domain.Entities;
using OrderService.Infrastructure.Repositories;
using OrderService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderService.Services.Order;
using AutoMapper;
using OrderService.Services.Broker;
using Moq;
using Microsoft.Extensions.Logging;

namespace UnitTest
{
    public class OrderServiceTest
    {
        private  IOrderService _orderService;
        private IGenericRepository<OrderEntity> _orderRepository;
        private IGenericRepository<ProductEntity> _productRepository;
        private IGenericRepository<OrderProductEntity> _orderProductRepo;

        [SetUp]
        public void Setup()
        {
            var db = new OrderServiceDBContext();

            _orderRepository = new GenericRepository<OrderEntity>(db);
            _productRepository = new GenericRepository<ProductEntity>(db);
            _orderProductRepo = new GenericRepository<OrderProductEntity>(db);

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<OrderServiceAutomapperProfile>())
                        .CreateMapper();

            var mockBrocker = new Mock<IBroker>().Object;
            var mockLogger = new Mock<ILogger<OrderService.Services.Order.OrderService>>().Object;

            _orderService = new OrderService.Services.Order.OrderService(
                _orderRepository,
                new GenericRepository<OrderProductEntity>(db),
                mockBrocker,
                mapper,
                mockLogger);
        }

        [Test]
        public void TestRepositoryInsertIntoOrders()
        {
            var customerNum = "+73312344567";
            var orderDesc = "Order N765";

            _productRepository.Add(new ProductEntity { Name = "Product1", Price = 200 });

            _orderService.CreateOrder(
                new OrderService.Domain.DTOs.CreateOrderDto
                {
                    Address = "st.Petersburg",
                    CustomerName = "Ivan Ivanov",
                    CustomerPhoneNumber = customerNum,
                    Description = orderDesc,
                    ProductIds = [1, 1]
                });

            var res1 = _orderRepository.Get().FirstOrDefault(o => o.CustomerPhoneNumber.Equals(customerNum) && o.Description.Equals(orderDesc));
            Assert.That(res1, Is.Not.Null);

            if (res1 != null) {
                var res2 = _orderProductRepo.Get().FirstOrDefault(o => o.OrderId.Equals(res1.Id));
                Assert.That(res2, Is.Not.Null);
                Assert.That(res2.Count, Is.EqualTo(2));
            }

        }
    }
}
