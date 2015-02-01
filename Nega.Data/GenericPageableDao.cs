using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Data
{
    public class GenericPageableDao<T> : GenericDao<T>, IPageableDao<T>
    {
        public Paging<T> Page(PagingRequest request, params object[] values)
        {
            return new Paging<T>();
        }
    }
}
