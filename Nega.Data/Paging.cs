using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Data
{

    public class Paging<T>
    {

        public long TotalCount { get; set; }

        public IEnumerable<T> Collection { get; set; }

    }

}
