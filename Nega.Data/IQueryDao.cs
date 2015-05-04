using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Data
{

    public interface IQueryDao<T, TId> : IDao<T, TId>
    {

        /// <summary>
        /// 查找符合输入条件的对象个数。
        /// </summary>
        /// <param name="values">输入的条件数组。</param>
        /// <returns>返回对象个数。</returns>
        long Count(params object[] values);

        /// <summary>
        /// 查找符合输入条件的对象集。
        /// </summary>
        /// <param name="values">输入的条件数组。</param>
        /// <returns>返回对象集。</returns>
        IList<T> List(params object[] values);

    }

}
