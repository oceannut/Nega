using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Data
{
    public class GenericQueryDao<T> : GenericDao<T>, IQueryDao<T>
    {

        public virtual long Count(params object[] values)
        {
            return 0;
        }

        public virtual IList<T> List(params object[] values)
        {
            return new List<T>();
        }

    }
}
