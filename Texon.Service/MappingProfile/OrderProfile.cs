using AutoMapper;
using Microsoft.Extensions.Configuration;
using Texon.Domin.Entities.Order;
using Texon.Shared.AddressDTo;
using Texon.Shared.OrderDto;

namespace Texon.Service.MappingProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile(IConfiguration configuration)
        {
            CreateMap<OrderAddress, AddressDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }



        public class ProductResolverPhoto(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string?>
        {

            public string? Resolve(OrderItem source, OrderItemDto destination, string? destMember, ResolutionContext context)
            {
                if (string.IsNullOrWhiteSpace(source.photo))
                    return null;
                return $"{configuration["BaseUrl"]}{source.photo}";


            }
        }
    }
}
