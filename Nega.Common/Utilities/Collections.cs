using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    /// <summary>
    /// 定义了与集合相关的操作。
    /// </summary>
    public static class Collections
    {

        /// <summary>
        /// 根据参照集合把指定的集合分支成多余、缺失、相似的3个子集。
        /// </summary>
        /// <typeparam name="T">元素类型。</typeparam>
        /// <typeparam name="Tkey">元素关键值类型，用来比较2个集合的元素。</typeparam>
        /// <param name="col">指定的集合。</param>
        /// <param name="comparand">参照集合。</param>
        /// <param name="keyAccessor">关键值访问定义。</param>
        /// <param name="extraItems">多余子集。</param>
        /// <param name="missingItems">缺失子集。</param>
        /// <param name="similarItems">相似子集。</param>
        public static void ForkSubset<T, Tkey>(ICollection<T> col,
            ICollection<T> comparand,
            Func<T, Tkey> keyAccessor,
            out IEnumerable<T> extraItems,
            out IEnumerable<T> missingItems,
            out IEnumerable<T> similarItems)
        {
            if (keyAccessor == null)
            {
                throw new ArgumentNullException();
            }

            ICollection<T> extraList = null;
            ICollection<T> missingList = null;
            ICollection<T> similarList = null;
            if (col != null && col.Count > 0)
            {
                if (comparand != null && comparand.Count > 0)
                {
                    extraList = new List<T>();
                    similarList = new List<T>();
                    Dictionary<Tkey, T> dict = comparand.ToDictionary<T, Tkey>(keyAccessor);
                    foreach (var item in col)
                    {
                        Tkey key = keyAccessor(item);
                        if (dict.ContainsKey(key))
                        {
                            similarList.Add(item);
                            dict.Remove(key);
                        }
                        else
                        {
                            extraList.Add(item);
                        }
                    }
                    if (dict.Count > 0)
                    {
                        missingList = new List<T>();
                        foreach (var item in dict.Values)
                        {
                            missingList.Add(item);
                        }
                        dict.Clear();
                    }
                }
                else
                {
                    extraList = col;
                }
            }
            else
            {
                if (comparand != null && comparand.Count > 0)
                {
                    missingList = comparand;
                }
            }

            extraItems = extraList;
            missingItems = missingList;
            similarItems = similarList;
        }


    }

}
