using AutoMapper;
using Texon.Domin.Entities.Baskets;
using Texon.Shared.BasketsDto;

namespace Texon.Service.MappingProfile
{
    public class BaketProfile : Profile
    {
        public BaketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();


            CreateMap<BasketItemDto, BasketItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId)) // لو الأسماء مختلفة
                .ReverseMap();
        }

    }
}
