using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Nega.WcfCommon
{

    public static class WebOperationContextHelper
    {

        public static string GetUserToken()
        {
            return WebOperationContext.Current.IncomingRequest.Headers[HttpRequestHeader.Authorization];
        }

    }

}
