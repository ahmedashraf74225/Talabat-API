using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Repositories;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository 
    {
        private readonly StackExchange.Redis.IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket= await _database.StringGetAsync(basketId);
            return basket.IsNull? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        // [Create & Update]
        public async Task<CustomerBasket?> UpdateBaksetAsync(CustomerBasket basket)
        {
            var CreateOrUpdated= await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(1));
            if (!CreateOrUpdated) return null;
            return await GetBasketAsync(basket.Id);

        }
    }
}
