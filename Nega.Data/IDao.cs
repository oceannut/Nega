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
    public interface IDao<T>
    {

        /// <summary>
        /// 保存对象信息。
        /// </summary>
        /// <param name="entity">对象信息。</param>
        /// <returns>返回true表示数据被保存；否则返回false表示数据未被保存。</returns>
        bool Save(T entity);

        /// <summary>
        /// 保存多个对象信息。
        /// </summary>
        /// <param name="col">对象信息集合。</param>
        void Save(ICollection<T> col);

        /// <summary>
        /// 更新对象信息。
        /// </summary>
        /// <param name="entity">对象信息。</param>
        /// <returns>返回true表示数据被更新；否则返回false表示无数据被更新。</returns>
        bool Update(T entity);

        /// <summary>
        /// 更新多个对象信息。
        /// </summary>
        /// <param name="col">对象信息集合。</param>
        void Update(ICollection<T> col);

        /// <summary>
        /// 删除对象信息。
        /// </summary>
        /// <param name="id">对象信息标识。</param>
        /// <returns>返回true表示数据被删除；否则返回false表示无数据被删除。</returns>
        bool Delete(object id);

        /// <summary>
        /// 删除多个对象信息。
        /// </summary>
        /// <param name="col">对象信息标识集合。</param>
        void Delete(ICollection<object> col);

        /// <summary>
        /// 获取对象信息。
        /// </summary>
        /// <param name="id">对象信息标识。</param>
        /// <returns>返回对象信息。</returns>
        T Get(object id);

        /// <summary>
        /// 判断对象是否存在，存在返回true，否则不存在返回false。
        /// </summary>
        /// <param name="id">对象信息标识。</param>
        /// <returns>对象存在返回true，否则不存在返回false。</returns>
        bool IsExist(object id);

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
