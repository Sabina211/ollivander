using Basket.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public BasketRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(Guid id)
        {
            return await _database.KeyDeleteAsync(id.ToString());
        }

        public async Task<CustomerBasket> GetBasketAsync(Guid customerId)
        {
            var data = await _database.StringGetAsync(customerId.ToString());

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<CustomerBasket>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.CustomerId.ToString(), JsonSerializer.Serialize(basket));
            return await GetBasketAsync((Guid) basket.CustomerId);
        }
    }
}
