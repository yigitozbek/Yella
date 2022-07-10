using AutoMapper;
using Yella.Order.Data.Orders.Dtos;
using Yella.Order.Domain.Orders;

namespace Yella.Order.Application.Modules.Orders.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem, OrderItemDTO>();
            CreateMap<OrderItemDTO, OrderItem>();
        }
    }
}
