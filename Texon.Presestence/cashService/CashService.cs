
using StackExchange.Redis;
using System.Text.Json;
using Texon.Service.Abstraction.IService;

namespace Texon.Persistence.cashService
{
    public class CashService (IConnectionMultiplexer connectionMultiplexer)
        : ICashService
    {
private readonly IDatabase database = connectionMultiplexer.GetDatabase();

        public async Task<string?> GetCashAsync(string key)
        {
            return await database.StringGetAsync(key);
        }

        public async Task SetCashAsync(string key, object value, TimeSpan timeToLive)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            await database.StringSetAsync(key, JsonSerializer.Serialize(value,options), timeToLive);
        }
    }
}
