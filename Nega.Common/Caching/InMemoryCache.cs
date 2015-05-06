using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public class InMemoryCache : ICache
    {

        private MemoryCache cache;

        private string name;
        public string Name
        {
            get { return name; }
        }

        public long Count
        {
            get { return this.cache.GetCount(name); }
        }

        public DateTimeOffset AbsoluteExpiration { get; set; }

        public TimeSpan SlidingExpiration { get; set; }

        public InMemoryCache(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }

            this.name = name;
            this.cache = new MemoryCache(name);
        }

        public void Add(string key, object obj)
        {
            CacheItemPolicy cacheItemPolicy  = new CacheItemPolicy();
            if (AbsoluteExpiration != ObjectCache.InfiniteAbsoluteExpiration)
            {
                cacheItemPolicy.AbsoluteExpiration = new DateTimeOffset();
            }
            else if (SlidingExpiration != ObjectCache.NoSlidingExpiration)
            {
                cacheItemPolicy.SlidingExpiration = new TimeSpan();
            }
            else
            {
                cacheItemPolicy.AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration;
            }
            this.cache.Set(key, obj, cacheItemPolicy, this.name);
        }

        public object Remove(string key)
        {
            return this.cache.Remove(key, this.name);
        }

        public object Get(string key)
        {
            return this.cache.Get(key, this.name);
        }

        public void Dispose()
        {
            this.cache.Dispose();
        }

    }

}
