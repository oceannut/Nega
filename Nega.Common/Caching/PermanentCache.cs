using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public class PermanentCache : ICache
    {

        private ConcurrentDictionary<string, object> cache;

        private string name;
        public string Name
        {
            get { return name; }
        }

        public long Count
        {
            get { return this.cache.Count; }
        }

        public IEnumerable<KeyValuePair<string, object>> Items
        {
            get
            {
                foreach (var item in cache)
                {
                    yield return item;
                }
            }
        }

        public PermanentCache(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }

            this.name = name;
            this.cache = new ConcurrentDictionary<string, object>();
        }

        public void Add(string key, object obj)
        {
            if (string.IsNullOrWhiteSpace(key) || obj == null)
            {
                throw new ArgumentNullException();
            }

            this.cache.TryAdd(key, obj);
        }

        public object Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException();
            }

            object value;
            if (this.cache.TryRemove(key, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public object Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException();
            }

            object value;
            if (this.cache.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public void Clear()
        {
            this.cache.Clear();
        }

        public void Dispose()
        {
            Clear();
        }

    }
}
