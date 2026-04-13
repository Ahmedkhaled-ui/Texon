namespace Texon.Domin.Entities.Order
{
    public class OrderItem : BaseEntity<Guid>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string photo { get; set; }
    }
}   
