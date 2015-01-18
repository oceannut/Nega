using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;

namespace Nega.WcfUnity
{
    public class UnityServiceHost : ServiceHost
    {

        public UnityServiceHost(IUnityContainer container,
            Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            foreach (var cd in this.ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new UnityInstanceProvider(container));
            }
        }


    }
}
