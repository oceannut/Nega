using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{
    public class ClientPrincipal : GenericPrincipal
    {

        public string Client { get; private set; }

        public ClientPrincipal(IIdentity identity, string[] roles, string client)
            : base(identity, roles)
        {
            if (string.IsNullOrWhiteSpace(client))
            {
                throw new ArgumentNullException();
            }

            this.Client = client;
        }

    }
}
