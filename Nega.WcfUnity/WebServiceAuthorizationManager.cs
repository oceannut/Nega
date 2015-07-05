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
            var ctx = WebOperationContext.Current;
            var token = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (string.IsNullOrEmpty(token))
            {
                allowed = true;
                //ctx.OutgoingResponse.StatusCode = HttpStatusCode.MethodNotAllowed;
            }
            else
            {
                allowed = true;
                //IPrincipalProvider principalProvider = container.Resolve<IPrincipalProvider>();
                //string username = auth;

                //if (!principalProvider.Authenticate(OperationContextHelper.GetIP(operationContext), username, username))
                //{
                //    ctx.OutgoingResponse.StatusCode = HttpStatusCode.MethodNotAllowed;
                //}
                //else
                //{
                //    allowed = true;
                //}
            }

            return allowed;  
        }

    }
}
