using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public class CacheManager
    {

        public static readonly ICacheFactory DefaultFactory = new InMemoryCacheFactory();

        private Dictionary<string, ICache> registry = new Dictionary<string, ICache>();

        private ICacheFactory factory = DefaultFactory;
        public ICacheFactory Factory
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                if (factory != value)
                {
                    factory = value;
                }
            }
        }

        public ICache GetCache(string name)
        {
            ICache cache = null;
            registry.TryGetValue(name, out cache);
            if (cache == null)
            {
                cache = factory.Create(name);
                registry.Add(name, cache);
            }

            return cache;
        }

    }

}
