using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nega.WcfCommon
{
    public static class WebHelper
    {

        public static KeyValuePair<HttpStatusCode,string> GetStatusCodeAndMessage(WebException ex)
        {
            HttpStatusCode code= HttpStatusCode.InternalServerError;
            string message = string.Empty;
            if (ex != null)
            {
                HttpWebResponse response = ex.Response as HttpWebResponse;
                code = response.StatusCode;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    message = reader.ReadToEnd();
                }
            }

            return new KeyValuePair<HttpStatusCode, string>(code, message);
        }

    }
}
