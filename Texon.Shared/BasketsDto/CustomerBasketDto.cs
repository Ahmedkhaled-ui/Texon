namespace Texon.Shared.BasketsDto
{
    public class CustomerBasketDto
    {
        public int Id { get; set; }
        public ICollection<BasketItemDto> basketItems { get; set; }

    }
}
