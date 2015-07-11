using Nega.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using R = Nega.WpfCommon.Properties.Resources;

namespace Nega.WpfCommon
{

    public static class WebExceptionHelper
    {

        public static KeyValuePair<HttpStatusCode, string> GetStatusCodeAndMessage(WebException ex)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
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

        public static void Handle(WebException ex,
            ILogger logger = null)
        {
            if (ex != null)
            {
                KeyValuePair<HttpStatusCode, string> status = GetStatusCodeAndMessage(ex);
                if (status.Key == HttpStatusCode.BadRequest)
                {
                    MessageBoxHelper.ShowError(status.Value);
                }
                else
                {
                    ILogger _logger = logger;
                    if (_logger == null)
                    {
                        _logger = LogManager.GetLogger();
                    }
                    _logger.Log(ex);
                    MessageBoxHelper.ShowError(R.DefaultServiceExceptionMessage);
                }
            }
        }

    }
}
