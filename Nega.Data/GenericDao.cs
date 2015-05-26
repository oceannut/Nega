using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nega.Data
{

    public class GenericDao<T, TId> : IDao<T, TId>
    {

        public virtual int Save(T entity)
        {
            return 0;
        }

        public virtual int Save(IEnumerable<T> col)
        {
            return 0;
        }

        public virtual int Update(T entity)
        {
            return 0;
        }

        public virtual int Update(IEnumerable<T> col)
        {
            return 0;
        }

        public virtual int Delete(TId id)
        {
            return 0;
        }

        public virtual int Delete(IEnumerable<TId> col)
        {
            return 0;
        }

        public virtual int Delete(T entity)
        {
            return 0;
        }

        public virtual int Delete(IEnumerable<T> col)
        {
            return 0;
        }

        public virtual T Get(TId id)
        {
            return default(T);
        }

        public virtual bool IsExist(TId id)
        {
            return false;
        }

    }

}
