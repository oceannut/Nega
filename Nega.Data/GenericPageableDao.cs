using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Data
{
    public class GenericPageableDao<T, TId> : GenericDao<T, TId>, IPageableDao<T, TId>
    {
        public virtual Paging<T> Page(PagingRequest request, params object[] values)
        {
            return new Paging<T>();
        }
    }
}
