using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersPasswordStore.Application.Interfaces.ICache
{
    public interface IMemoryCacheService
    {
        /// <summary>
        /// get the value of a key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>value of key</returns>
        List<T?> Get<T>(string key);

        /// <summary>
        /// try to get the key value if existed
        /// </summary>
        /// <param name="key"></param>
        /// <param name="T"></param>
        /// <returns>return true if existed and false otherwise</returns>
        bool TryGetValue(string key, out string T);


        /// <summary>
        /// Set function with fixed sliding and absolute expiration time.one minute for sliding and one hour for absolute expiration time.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">unique identity</param>
        /// <param name="value"></param>
        /// <returns></returns>
        T? Set<T>(string key, T value);


        /// <summary>
        /// determine absolute expiration time in minute in Set function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">unique identity</param>
        /// <param name="value"></param>
        /// <param name="absoluteExpirationMinutes"></param>
        /// <returns></returns>
        T? Set<T>(string key, T value, int absoluteExpirationMinutes);

        /// <summary>
        /// determine absolute expiration time and sliding expiration time in minute in Set function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">unique identity</param>
        /// <param name="value"></param>
        /// <param name="absoluteExpirationMinutes"></param>
        /// <param name="slidingExpirationMinutes"></param>
        /// <returns></returns>
        T? Set<T>(string key, T value, int absoluteExpirationMinutes, int slidingExpirationMinutes);

        /// <summary>
        /// remove key from cache
        /// </summary>
        /// <param name="key"></param>
        void Reset(string key);
    }
}