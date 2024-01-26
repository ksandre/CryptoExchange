using AutoMapper;
using CryptoExchange.Application.Features.Order.Commands.CreateOrder;
using CryptoExchange.Application.Features.Order.Commands.UpdateOrder;
using CryptoExchange.Application.Features.Order.Queries.GetOrderDetails;
using CryptoExchange.Application.Features.Order.Queries.GetOrders;
using CryptoExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Application.MappingProfiles
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<Order, OrderDetailsDto>();
            CreateMap<CreateOrderCommand, Order>();
            CreateMap<UpdateOrderCommand, Order>();
        }
    }
}
