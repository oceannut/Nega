using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

using Nega.Common;
using System.ServiceModel;

namespace Nega.WcfCommon
{

    public static class ExceptionHelper
    {

        public static FaultException Replace(Exception ex, string message = null, ILogger logger = null)
        {
            if (ex != null)
            {
                DoLog(ex, logger);

                if (string.IsNullOrWhiteSpace(message))
                {
                    return new WebFaultException(HttpStatusCode.InternalServerError);
                }
                else
                {
                    return new WebFaultException<string>(message, HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public static WebFaultException<T> Replace<T>(Exception ex, T message, ILogger logger = null)
        {
            if (ex != null)
            {
                DoLog(ex, logger);

                return new WebFaultException<T>(message, HttpStatusCode.InternalServerError);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        private static void DoLog(Exception ex, ILogger logger = null)
        {
            ILogger _logger = logger;
            if (_logger == null)
            {
                _logger = LogManager.GetLogger();
            }
            _logger.Log(ex);
        }

    }

}
