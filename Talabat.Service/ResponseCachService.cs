using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class ResponseCachService : IResponseCachService
    {
        private readonly IDatabase _database;

        public ResponseCachService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CachResponseAsync(string cachKey, object response, TimeSpan liveTime)
        {
            if (response is null) return;

            var responseOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var responseSerialized = JsonSerializer.Serialize(response , responseOptions);

            await _database.StringSetAsync(cachKey, responseSerialized, liveTime);
          
        }

        public async Task<string> GetCachedResponseAsync(string cachKey)
        {
            var cachedResponse = await _database.StringGetAsync(cachKey);

            if (cachedResponse.IsNullOrEmpty) return null;
            return cachedResponse;
        }
    }
}
