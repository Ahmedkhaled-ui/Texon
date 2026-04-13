using AutoMapper;
using Texon.Domin.Entities.Products;
using Texon.Shared;

namespace Texon.Service.MappingProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {

            CreateMap<Category, CategoryDto>();
              
            CreateMap<CategoryDto, Category>().ForMember(dest=>dest.Id,opt=>opt.Ignore());


        }
    }
}
