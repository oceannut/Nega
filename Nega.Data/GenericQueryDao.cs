using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Data
{
    public class GenericQueryDao<T, TId> : GenericDao<T, TId>, IQueryDao<T, TId>
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
