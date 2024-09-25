using OrderService.Domain.Entities;
using OrderService.Infrastructure.Repositories;

namespace OrderService.App
{
    public static class OrderServiceBuilderExtensions
    {
        public static IServiceCollection InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IGenericRepository<OrderEntity>, GenericRepository<OrderEntity>>();
            services.AddTransient<IGenericRepository<ProductEntity>, GenericRepository<ProductEntity>>();
            services.AddTransient<IGenericRepository<OrderProductEntity>, GenericRepository<OrderProductEntity>>();
            return services;
        }
    }
}
