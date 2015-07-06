using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

using Microsoft.Practices.Unity;

using Nega.Common;
using System.ServiceModel.Channels;
using Nega.WcfCommon;

namespace Nega.WcfUnity
{
    public class WebServiceAuthorizationManager : ServiceAuthorizationManager
    {

        private readonly IUnityContainer container;

        public WebServiceAuthorizationManager()
        {
            container = ObjectsRegistry.SoloInstance.Container;
        }

        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            bool allowed = false;
            var token = WebOperationContextHelper.GetUserToken();
            if (string.IsNullOrEmpty(token))
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.MethodNotAllowed;
            }
            else
            {
                IClientManager clientManager = container.Resolve<IClientManager>();
                Client client = clientManager.GetClientByUserToken(token);
                if (client != null)
                {
                    allowed = true;
                }
                else
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.MethodNotAllowed;
                }
            }

            return allowed;  
        }

    }
}
