using Microsoft.AspNetCore.Http;

namespace Texon.Shared.ProductDto
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }

        public string CategoryName { get; set; }

        public List<string> GalleryUrls { get; set; } = new();
        public int StockQuantity { get; set; }

        public bool IsOnSale => DiscountPrice.HasValue && DiscountPrice < Price;
        public bool IsInStock => StockQuantity > 0;
        public int DiscountPercentage => (Price > 0 && DiscountPrice.HasValue && DiscountPrice < Price)
        ? (int)((Price - DiscountPrice.Value) / Price * 100)
        : 0;

        public decimal SavingsAmount => (DiscountPrice.HasValue && DiscountPrice < Price)
            ? (Price - DiscountPrice.Value)
            : 0;

        public bool IsVisible { get; set; }
    }
}