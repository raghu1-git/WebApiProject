using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Caching
{
    public class RedisCacheService : ICachingService
    {
        private readonly IDistributedCache _distributedCache;
        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<T> GetCacheAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                return default!;

            var data = await _distributedCache.GetStringAsync(key);
            if(data == null)
                return default!;

            return JsonSerializer.Deserialize<T>(data)!;
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task SetCacheAsync<T>(string key, T value, int timeSpanInMinutes)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(timeSpanInMinutes)
            };
            
            var data = JsonSerializer.Serialize(value);
            await _distributedCache.SetStringAsync(key, data, options);
        }
    }
}
