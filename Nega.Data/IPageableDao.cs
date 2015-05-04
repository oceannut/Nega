using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Data
{

    public interface IPageableDao<T, TId> : IDao<T, TId>
    {

        Paging<T> Page(PagingRequest request, params object[] values);

    }

}
