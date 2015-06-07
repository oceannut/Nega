using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Nega.Entlib
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class ResourceAuthorizationCallHandlerAttribute : HandlerAttribute
    {

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new ResourceAuthorizationCallHandler(container);
        }

    }

}
