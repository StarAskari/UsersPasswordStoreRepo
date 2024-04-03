using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersPasswordStore.Application.Interfaces.ICache
{
    public interface IMemoryCacheService
    {
        List<T?> Get<T>(string key);
        bool TryGetValue(string key, out string T);
        
        T? Set<T>(string key, T value);
        T? Set<T>(string key, T value, int absoluteExpirationMinutes);
        T? Set<T>(string key, T value, int absoluteExpirationMinutes, int slidingExpirationMinutes);
        void Remove(object key);
    }
}
