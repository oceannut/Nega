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

    public class SecureUnityServiceHostFactory : UnityServiceHostFactory
    {

        public SecureUnityServiceHostFactory() : base()
        {
           
        }

        protected override ServiceHost CreateServiceHost(Type serviceType,
            Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(serviceType, baseAddresses);
            host.Authorization.ServiceAuthorizationManager = container.Resolve<ServiceAuthorizationManager>();
            return host;
        }

    }

}
