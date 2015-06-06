using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity.InterceptionExtension;

namespace Nega.Entlib
{

    public class AuthorizationCallHandler : ICallHandler
    {

        public int Order { get; set; }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            throw new NotImplementedException();
        }

    }

}
