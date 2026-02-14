using Texon.Domin.Entities.Baskets;

namespace Texon.Domin.Contracts
{
    public interface IBasketRepository
    
        {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket , TimeSpan? TTL=null);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
