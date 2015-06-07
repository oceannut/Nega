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

    [AddSateliteProviderCommand("resourceAuthorization")]
    public class ResourceAuthorizationCallHandlerData : CallHandlerData
    {

        public ResourceAuthorizationCallHandlerData()
        {
            Type = typeof(ResourceAuthorizationCallHandlerAttribute);
        }

        public ResourceAuthorizationCallHandlerData(string handlerName)
            : base(handlerName, typeof(ResourceAuthorizationCallHandlerAttribute))
        {
        }

        public ResourceAuthorizationCallHandlerData(string handlerName, int handlerOrder)
            : base(handlerName, typeof(ResourceAuthorizationCallHandlerAttribute))
        {
            this.Order = handlerOrder;
        }

        protected override void DoConfigureContainer(IUnityContainer container, string registrationName)
        {
            container.RegisterType<ICallHandler, ResourceAuthorizationCallHandler>(
                registrationName,
                new InjectionConstructor(),
                new InjectionProperty("Order", this.Order));
        }

    }

}
