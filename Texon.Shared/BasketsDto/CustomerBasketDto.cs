namespace Texon.Shared.BasketsDto
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public ICollection<BasketItemDto> basketItems { get; set; }

    }
}
