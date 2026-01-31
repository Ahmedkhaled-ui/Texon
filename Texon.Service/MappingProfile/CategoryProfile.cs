using AutoMapper;
using Texon.Domin.Entities.Products;
using Texon.Shared;

namespace Texon.Service.MappingProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {

            CreateMap<Category, CategoryDto>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                  Thread.CurrentThread.CurrentCulture.Name.StartsWith("ar")
                  ? src.NameAr
                  : src.NameEn))
              .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
              .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn));


        }
    }
}
