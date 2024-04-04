using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UsersPasswordStore.Application.Interfaces.ICache;

namespace UsersPasswordStore.Application.Services.Cache
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public List<T?> Get<T>(string key)
        {
            var value = _memoryCache.Get(key);

            if (value != null)
            {
                var cachedString = Encoding.UTF8.GetString((byte[])value);
                if (cachedString != "Newtonsoft.Json.JsonSerializer")
                {

                    return JsonConvert.DeserializeObject<List<T>>(cachedString); ;
                }
            }

            return default(List<T>);
        }


        public T? Set<T>(string key, T value)
        {
            // Reset(key);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                     .SetSlidingExpiration(TimeSpan.FromMinutes(180))
                     .SetAbsoluteExpiration(TimeSpan.FromMinutes(360))
                     .SetPriority(CacheItemPriority.Normal)
                     .SetSize(1024);

            var cachedString = System.Text.Json.JsonSerializer.Serialize(value);
            var newDataToCache = Encoding.UTF8.GetBytes(cachedString);

            _memoryCache.Set(key, newDataToCache, cacheEntryOptions);

            return default;
        }


        public T? Set<T>(string key, T value, int absoluteExpirationMinutes)
        {

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(180))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(360))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);

            var cachedString = JsonConvert.SerializeObject(value);

            var newDataToCache = Encoding.UTF8.GetBytes(cachedString);

            _memoryCache.Set(key, newDataToCache, cacheEntryOptions);
            return default;
        }


        public T Set<T>(string key, T value, int absoluteExpirationMinutes, int slidingExpirationMinutes)
        {

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                     .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpirationMinutes))
                     .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteExpirationMinutes))
                     .SetPriority(CacheItemPriority.Normal)
                     .SetSize(1024);

            var cachedString = JsonConvert.SerializeObject(value);
            var newDataToCache = Encoding.UTF8.GetBytes(cachedString);

            _memoryCache.Set(key, newDataToCache, cacheEntryOptions);

            return default;
        }


        public bool TryGetValue(string key, out string T)
        {

            return _memoryCache.TryGetValue(key, out T);
        }


        public void Reset(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}