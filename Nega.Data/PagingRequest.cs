using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Data
{

    public class PagingRequest
    {

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<Sortable> Sorts { get; set; }

        public bool NeedTotalCount { get; set; }

        public PagingRequest()
        {
            this.NeedTotalCount = true;
        }

        public PagingRequest(int pageIndex, int pageSize)
            : this()
        {
            if (pageIndex < 0 || pageSize < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public PagingRequest(int pageIndex, int pageSize, IEnumerable<Sortable> sorts)
            : this(pageIndex, pageSize)
        {
            if (sorts != null && sorts.Count() > 0)
            {
                this.Sorts = sorts.ToList();
            }
        }

    }

}
