using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nega.Common
{

    /// <summary>
    /// 定义了数据是否被废弃的特性及操作。
    /// </summary>
    /// <typeparam name="T">数据类型。</typeparam>
    public interface IDisuseable<T>
    {

        /// <summary>
        /// 指示是否被废弃；true表示废弃，不再使用；false表示未废弃，仍在使用。
        /// </summary>
        bool Disused { get; }

        /// <summary>
        /// 放弃使用。
        /// </summary>
        /// <param name="action">操作定义。</param>
        void Disuse(Action<T> action);

        /// <summary>
        /// 恢复使用。
        /// </summary>
        /// <param name="action">操作定义。</param>
        void Use(Action<T> action);

    }

}
