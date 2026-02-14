using StackExchange.Redis;
using System.Text.Json;
using Texon.Domin.Contracts;
using Texon.Domin.Entities.Baskets;

namespace Texon.Persistence.Repository
{
    internal class BasketRepository(IConnectionMultiplexer multiplexer) : IBasketRepository
    {
        private readonly IDatabase _database = multiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var json = await _database.StringGetAsync(basketId);
            if (json.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<CustomerBasket>(json!);

        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket, TimeSpan? TTL = null)
        {
            var json = JsonSerializer.Serialize(basket);
          await  _database.StringSetAsync(basket.Id, json, TTL?? TimeSpan.FromDays(30));
return  (await GetBasketAsync(basket.Id))!;


        }
    }
}
