using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nega.Common;

namespace Nega.WcfCommon
{

    public interface IClientManager
    {

        Client AddClient(string username, string userToken = null, string[] roles = null);

        Client RemoveClient(string username);

        Client GetClient(string username);

        Client GetClientByUserToken(string userToken);

    }

}
