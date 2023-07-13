using AutoMapper;
using e_catalog_backend.Dtos.Order;
using e_catalog_backend.Models;

namespace e_catalog_backend.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, ReadOrderDto>();
        CreateMap<CreateOrderDto, Order>(); 
    }
}

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<OrderItem, ReadOrderItemDto>();
        CreateMap<CreateOrderItemDto, OrderItem>();
    }
}