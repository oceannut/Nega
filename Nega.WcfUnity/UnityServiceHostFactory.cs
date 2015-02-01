using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Activation;

using Microsoft.Practices.Unity;

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
            return new UnityServiceHost(this.container, serviceType, baseAddresses);
        }

    }

}
