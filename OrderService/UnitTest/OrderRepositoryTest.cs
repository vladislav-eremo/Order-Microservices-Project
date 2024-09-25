using OrderService.Domain.Entities;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Repositories;
using OrderService.Services.Order;

namespace UnitTest
{
    public class OrderRepositoryTest
    {
        private IGenericRepository<OrderEntity> _repository;

        [SetUp]
        public void Setup()
        {
            var db = new OrderServiceDBContext();
            _repository = new GenericRepository<OrderEntity>(db);
        }

        [Test]
        public void TestRepositoryInsertIntoOrders()
        {
            var description = "Order N545";
            var customerNumber = "+375291234567";

            _repository.Add(new OrderEntity
            {
                CustomerName = "Ivan",
                Address = "Minsk, Sovetskaya 11, 55",
                Description = description,
                CustomerPhoneNumber = customerNumber
            });

            _repository.Save();

            var res = _repository.Get().FirstOrDefault(x => x.Description.Equals(description) && x.CustomerPhoneNumber.Equals(customerNumber));

            Assert.That(res, Is.Not.Null, "Inserted order must not be null");
        }
    }
}