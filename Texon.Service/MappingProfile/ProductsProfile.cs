using AutoMapper;
using Microsoft.Extensions.Configuration;
using Texon.Shared.ProductDto;

namespace Texon.Service.MappingProfile
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductResponse>()
    .ForMember(dest => dest.ProductName, opt => opt.MapFrom((src, dest, destMember, context) =>
    {
        var language = context.Items["lang"] as string ?? "en";
        return language == "ar" ? src.NameAr : src.NameEn;
    }))
    .ForMember(dest => dest.Description, opt => opt.MapFrom((src, dest, destMember, context) =>
    {
        var language = context.Items["lang"] as string ?? "en";
        return language == "ar" ? src.DescriptionAr : src.DescriptionEn;
    }))
    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom((src, dest, destMember, context) =>
    {
        var language = context.Items["lang"] as string ?? "en";
        return language == "ar" ? src.Category.NameAr : src.Category.NameEn;
    }))
    .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom( new ProductResolverPhoto(configuration)!))
                .ForMember(dest => dest.GalleryUrls, opt => opt.MapFrom(new ProductResolverPhotos(configuration)!));





            CreateMap<ProductRequest, Product>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore())

                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }

        public class ProductResolverPhoto(IConfiguration configuration) : IValueResolver<Product, ProductResponse, string?>
        {
            public string? Resolve(Product source, ProductResponse destination, string? destMember, ResolutionContext context)
            {
                if (string.IsNullOrWhiteSpace(source.PhotoUrl))

                    return null;

                return $"{configuration["BaseUrl"]}{source.PhotoUrl}";


            }
        }

        public class ProductResolverPhotos(IConfiguration configuration) : IValueResolver<Product, ProductResponse, List<string>>
        {
            public List<string> Resolve(Product source, ProductResponse destination, List<string> destMember, ResolutionContext context)
            {
                var baseUrl = configuration["BaseUrl"]; 

                if (source.Images != null && source.Images.Any())
                {
                    return source.Images
              .Select(img => $"{baseUrl}{img.ImageUrl}")
              .ToList();
                }

                return new List<string>();
            }
        }
    }
}
