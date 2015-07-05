using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Nega.WcfCommon
{

    public class ConnectionClient : Client
    {

        public IContextChannel Channel { get; private set; }

        public DateTime Timestamp { get; private set; }

        public void KeepAlive()
        {

        }

    }

}
