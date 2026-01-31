using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Texon.Shared.ProductDto
{
    public class ProductRequest
    {
        [Required]
        public string NameAr { get; set; }

        [Required]
        public string NameEn { get; set; }

        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }

        [Required]
        public string PhotoUrl { get; set; }
        public List<string> GalleryUrls { get; set; } = new List<string>(); 
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public bool IsVisible { get; set; } = true;
    }
}