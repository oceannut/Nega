using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;

using Microsoft.Practices.Unity;

namespace Nega.WcfUnity
{
    public class UnityInstanceProvider : IInstanceProvider, IContractBehavior
    {

        private readonly IUnityContainer container;

        public UnityInstanceProvider(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        #region IInstanceProvider Members

        public object GetInstance(InstanceContext instanceContext,
          Message message)
        {
            return this.GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            try
            {
                object obj = this.container.Resolve(
                  instanceContext.Host.Description.ServiceType);
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void ReleaseInstance(InstanceContext instanceContext,
          object instance)
        {
            this.container.Teardown(instance);
        }

        #endregion

        #region IContractBehavior Members

        public void AddBindingParameters(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint,
          BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint,
          DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(
          ContractDescription contractDescription,
          ServiceEndpoint endpoint)
        {
        }

        #endregion


    }

}
