using Microsoft.Extensions.Caching.Memory;
using Sahibinden.Business.Abstract;
using Sahibinden.Business.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Concrete.Services
{
    public class CacheService : ICacheService
    {
        private static readonly MemoryCacheOptions _opts = new()
        { 
        };
        private readonly IMemoryCache _memoryCache = new MemoryCache(_opts);

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T GetFromCache<T>(string key)
        {
            return _memoryCache.TryGetValue(key, out T value) ? value : default;
        }

        public void RemoveFromCache(string key)
        {
            _memoryCache.Remove(key);
        }

        public void SetToCache<T>(string key, T value, TimeSpan expiration)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                
            .SetAbsoluteExpiration(expiration);
            _memoryCache.Set(key, value, cacheEntryOptions);
        }

        public bool TryGetUser(int userId, out (UserDto User, List<int> Permissions) cachedUser)
        {
            var key = $"User_{userId}";
            return _memoryCache.TryGetValue(key, out cachedUser);
        }
    }
}
