using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Nega.Entlib
{

    [AddSateliteProviderCommand("authorization")]
    public class AuthorizationCallHandlerData : CallHandlerData
    {

        public AuthorizationCallHandlerData()
        {
            Type = typeof(AuthorizationCallHandlerAttribute);
        }

        public AuthorizationCallHandlerData(string handlerName)
            : base(handlerName, typeof(AuthorizationCallHandlerAttribute))
        {
        }

        public AuthorizationCallHandlerData(string handlerName, int handlerOrder)
            : base(handlerName, typeof(AuthorizationCallHandlerAttribute))
        {
            this.Order = handlerOrder;
        }

        protected override void DoConfigureContainer(IUnityContainer container, string registrationName)
        {
            container.RegisterType<ICallHandler, AuthorizationCallHandler>(
                registrationName,
                new InjectionConstructor(),
                new InjectionProperty("Order", this.Order));
        }

    }

}
