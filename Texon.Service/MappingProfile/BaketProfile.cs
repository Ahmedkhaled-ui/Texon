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

        
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }

    }
}
