using AutoMapper;
using OrderService.Domain.DTOs;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Services.Order
{
    internal class OrderServiceAutomapperProfile : Profile
    {
        public OrderServiceAutomapperProfile() {
            CreateMap<OrderEntity, GetOrderDto>().ReverseMap();
            CreateMap<CreateOrderDto, OrderEntity>();
        }
    }
}
