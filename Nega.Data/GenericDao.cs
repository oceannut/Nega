using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nega.Data
{

    public class GenericDao<T> : IDao<T>
    {

        public virtual bool Save(T entity)
        {
            return false;
        }

        public virtual void Save(ICollection<T> col)
        {
            
        }

        public virtual bool Update(T entity)
        {
            return false;
        }

        public virtual void Update(ICollection<T> col)
        {
            
        }

        public virtual bool Delete(object id)
        {
            return false;
        }

        public virtual void Delete(ICollection<object> col)
        {
            
        }

        public virtual T Get(object id)
        {
            return default(T);
        }

        public virtual bool IsExist(object id)
        {
            return false;
        }

    }

}
