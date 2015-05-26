using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nega.Data
{

    /// <summary>
    /// 定义了对对象通用的增、删、改、查等基本的数据访问操作。
    /// </summary>
    /// <typeparam name="T">对象类型。</typeparam>
    /// <typeparam name="TId">对象标识类型。</typeparam>
    public interface IDao<T, TId>
    {

        /// <summary>
        /// 保存对象信息。
        /// </summary>
        /// <param name="entity">对象信息。</param>
        /// <returns>返回成功保存的对象个数。</returns>
        int Save(T entity);

        /// <summary>
        /// 保存多个对象信息。
        /// </summary>
        /// <param name="col">对象信息集合。</param>
        /// <returns>返回成功保存的对象个数。</returns>
        int Save(IEnumerable<T> col);

        /// <summary>
        /// 更新对象信息。
        /// </summary>
        /// <param name="entity">对象信息。</param>
        /// <returns>返回成功更新的对象个数。</returns>
        int Update(T entity);

        /// <summary>
        /// 更新多个对象信息。
        /// </summary>
        /// <param name="col">对象信息集合。</param>
        /// <returns>返回成功更新的对象个数。</returns>
        int Update(IEnumerable<T> col);

        /// <summary>
        /// 删除对象信息。
        /// </summary>
        /// <param name="id">对象信息标识。</param>
        /// <returns>返回成功删除的对象个数。</returns>
        int Delete(TId id);

        /// <summary>
        /// 删除多个对象信息。
        /// </summary>
        /// <param name="col">对象信息标识集合。</param>
        /// <returns>返回成功删除的对象个数。</returns>
        int Delete(IEnumerable<TId> col);

        /// <summary>
        /// 删除对象信息。
        /// </summary>
        /// <param name="entity">对象信息。</param>
        /// <returns>返回成功删除的对象个数。</returns>
        int Delete(T entity);

        /// <summary>
        /// 删除多个对象信息。
        /// </summary>
        /// <param name="col">对象信息集合。</param>
        /// <returns>返回成功删除的对象个数。</returns>
        int Delete(IEnumerable<T> col);

        /// <summary>
        /// 获取对象信息。
        /// </summary>
        /// <param name="id">对象信息标识。</param>
        /// <returns>返回对象信息。</returns>
        T Get(TId id);

        /// <summary>
        /// 判断对象是否存在，存在返回true，否则不存在返回false。
        /// </summary>
        /// <param name="id">对象信息标识。</param>
        /// <returns>对象存在返回true，否则不存在返回false。</returns>
        bool IsExist(TId id);

    }

}
