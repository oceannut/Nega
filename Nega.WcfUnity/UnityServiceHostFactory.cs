using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Activation;

using Microsoft.Practices.Unity;

using Nega.Common;

namespace Nega.WcfUnity
{

    public class UnityServiceHostFactory : ServiceHostFactory
    {

        private readonly IUnityContainer container;

        public UnityServiceHostFactory()
        {
            container = ObjectsRegistry.SoloInstance.Container;
        }

        protected override ServiceHost CreateServiceHost(Type serviceType,
            Uri[] baseAddresses)
        {
            var host = new UnityServiceHost(this.container, serviceType, baseAddresses);
            host.Authorization.ServiceAuthorizationManager = container.Resolve<ServiceAuthorizationManager>();
            return host;
        }

        public ServiceHost CreateServiceHost(Type serviceType)
        {
            return CreateServiceHost(serviceType, null);
        }

    }

}
