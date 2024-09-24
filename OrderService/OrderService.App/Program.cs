using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure;
using OrderService.Infrastructure.Repositories.Order;
using OrderService.Services.Broker;
using OrderService.Services.Broker.RabbitMQ;
using OrderService.Services.Order;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddLogging(builder => builder.AddConsole());
builder.Services.AddDbContext<OrderServiceDBContext>();

builder.Services.AddTransient<IBroker, RabbitMQService>();

builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderService, OrderService.Services.Order.OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
