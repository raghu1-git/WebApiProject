using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Caching
{
    public interface ICachingService
    {
        Task<T> GetCacheAsync<T>(string key);
        Task SetCacheAsync<T>(string key, T value, int timeSpanInMinutes);
        Task RemoveCacheAsync(string key);
    }
}
