using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nega.Common
{

    /// <summary>
    /// 定义了分类的特性。
    /// </summary>
    public interface ICategoryable
    {

        /// <summary>
        /// 上级分类标识。
        /// </summary>
        string ParentId { get; }

        /// <summary>
        /// 显示的排序序号。
        /// </summary>
        int Order { get; }

    }

}
