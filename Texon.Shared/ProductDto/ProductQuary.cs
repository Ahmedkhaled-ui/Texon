namespace Texon.Shared.ProductDto
{
    public class ProductQuary
    {
        private const int MaxPageSize = 20;
        private const int DefaultPageSize = 10;
        public int? categoryId { get; set; }
        public string? Search { get; set; }

        public ProductSort? Sort { get; set; }

        private int PageSize = DefaultPageSize;
        public int pageSize
        {

            get => PageSize;

            set => PageSize = value > MaxPageSize ? MaxPageSize : value < DefaultPageSize ? DefaultPageSize : value;
        }
        public int pageIndex { get; set; } = 1;
    }
}

public enum ProductSort
{
    PriceAsc = 1,
    PriceDesc = 2,
    NameAsc = 3,
    NameDesc = 4
}
