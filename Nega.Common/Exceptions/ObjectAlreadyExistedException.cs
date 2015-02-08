using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nega.Common
{
    public class ObjectAlreadyExistedException<TObj, TId> : BusinessException
    {

        public TObj Obj { get; private set; }

        public TId Id { get; private set; }

        public ObjectAlreadyExistedException(TObj obj, TId id)
            : base(string.Format("The object[id={0}] of {1} is existed.", id, typeof(TObj)))
        {
            this.Obj = obj;
            this.Id = id;
        }

    }
}
