
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Services.Generic
{
    public static class CacheService
    {
        private static readonly MemoryCache _cache = MemoryCache.Default;

        public static IList<T> GetOrSet<T>(string cacheKey, Func<long, IList<T>> getItemCallback, long pram) where T : class
        {
            IList<T> items = _cache.Get(cacheKey) as IList<T>;
            if (items == null)
            {
                items = getItemCallback(pram);
                _cache.Add(cacheKey, items, DateTime.Now.AddDays(1));
            }
            return items;
        }

        public static IList<T> GetOrSet<T>(string cacheKey, Func<string, IList<T>> getItemCallback, string pram) where T : class
        {
            IList<T> items = _cache.Get(cacheKey) as IList<T>;
            if (items == null)
            {
                items = getItemCallback(pram);
                _cache.Add(cacheKey, items, DateTime.Now.AddDays(1));
            }
            return items;
        }

        public static IList<T> GetOrSet<T>(string cacheKey, Func<IList<T>> getItemCallback) where T : class
        {
            IList<T> items = _cache.Get(cacheKey) as IList<T>;
            if (items == null)
            {
                items = getItemCallback();
                _cache.Add(cacheKey, items, DateTime.Now.AddDays(1));
            }
            return items;
        }

        //Store Stuff in the cache  
        private static void StoreCacheItems<T>(string _key, IList<T> items)
        {
            var cacheItemPolicy = new CacheItemPolicy()
            {
                //Set your Cache expiration.
                AbsoluteExpiration = DateTime.Now.AddDays(1)
            };
            _cache.Add(_key, items, cacheItemPolicy);
        }

        //Get stuff from the cache
        private static IList<T> GetCacheItems<T>(string _key)
        {
            if (!_cache.Contains(_key))
            {
                return null;
            }

            return _cache.Get(_key) as List<T>;
        }

        //Remove stuff from the cache. If no key supplied, all data will be erased.
        public static void ExpireCache(string _key)
        {
            if (string.IsNullOrEmpty(_key))
            {
                _cache.Dispose();
            }
            else
            {
                _cache.Remove(_key);
            }
        }
    }
}
