using AutoMapper;
using Texon.Domin.Entities.DeliveryMethod;
using Texon.Shared.DeliveryMethodDto;

namespace Texon.Service.MappingProfile
{
    public class DeliveryMethodProfile : Profile
    {
        public DeliveryMethodProfile()
        {
            CreateMap<DeliveryMethods, DeliveryMethodDto>();
            CreateMap<DeliveryMethodDto, DeliveryMethods>().ForMember(dest => dest.Id, opt => opt.Ignore());
            ;
        }
    }
}
