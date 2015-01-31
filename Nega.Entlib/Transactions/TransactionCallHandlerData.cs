using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Nega.Entlib
{

    [AddSateliteProviderCommand("transaction")]
    public class TransactionCallHandlerData : CallHandlerData
    {

        public TransactionCallHandlerData()
        {
            Type = typeof(TransactionCallHandlerAttribute);
        }

        public TransactionCallHandlerData(string handlerName)
            : base(handlerName, typeof(TransactionCallHandlerAttribute))
        {
        }

        public TransactionCallHandlerData(string handlerName, int handlerOrder)
            : base(handlerName, typeof(TransactionCallHandlerAttribute))
        {
            this.Order = handlerOrder;
        }

        protected override void DoConfigureContainer(IUnityContainer container, string registrationName)
        {
            container.RegisterType<ICallHandler, TransactionCallHandler>(
                registrationName,
                new InjectionConstructor(),
                new InjectionProperty("Order", this.Order));
        }

    }

}
