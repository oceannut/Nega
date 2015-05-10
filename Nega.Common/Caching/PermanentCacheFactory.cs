using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{
    public class PermanentCacheFactory : ICacheFactory
    {

        public ICache Create(string name)
        {
            return new PermanentCache(name);
        }

    }
}
