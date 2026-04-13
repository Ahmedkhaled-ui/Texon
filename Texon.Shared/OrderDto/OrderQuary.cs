namespace Texon.Shared.OrderDto
{
    public class OrderQuary
    {
        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 10;
        public string? Email { get; set; }
        public string? Status { get; set; } // Pending, Shipped, etc.
        public string? Search { get; set; } // للبحث برقم الأوردر مثلاً
        private int PageSize = DefaultPageSize;
        public int pageSize
        {

            get => PageSize;

            set => PageSize = value > MaxPageSize ? MaxPageSize : value < DefaultPageSize ? DefaultPageSize : value;
        }
        public int pageIndex { get; set; } = 1;
    }
}
