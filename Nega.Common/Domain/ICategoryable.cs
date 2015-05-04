using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nega.Common
{

    /// <summary>
    /// 定义了分类的特性。
    /// </summary>
    /// <typeparam name="TId">编号类型。</typeparam>
    public interface ICategoryable<TId>
    {

        /// <summary>
        /// 上级分类标识。
        /// </summary>
        TId ParentId { get; }

        /// <summary>
        /// 显示的排序序号。
        /// </summary>
        long Sequence { get; }

    }

}
