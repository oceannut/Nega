using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Nega.WcfCommon
{

    public class Client
    {

        public string Username { get; set; }

        public bool IsAuthenticated { get; set; }

        public string UserToken { get; set; }

        public string IP { get; set; }

    }

}
