using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public interface ICache : IDisposable
    {

        string Name { get; }

        long Count { get; }

        void Add(string key, object obj);

        object Remove(string key);

        object Get(string key);

    }

}
