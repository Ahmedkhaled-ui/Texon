namespace Texon.Domin.Entities.Baskets
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; } = [];

    }
}
