using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nega.Common
{

    public class GenericPrincipalProvider : IPrincipalProvider
    {

        private IAuthenticationProvider authenticationProvider;

        public GenericPrincipalProvider(IAuthenticationProvider authenticationProvider)
        {
            this.authenticationProvider = authenticationProvider;
        }

        public string Identity
        {
            get { throw new NotImplementedException(); }
        }

        public IPrincipal Principal
        {
            get { return Thread.CurrentPrincipal; }
        }

        public bool Authenticate(string client, string username, string pwd)
        {
            string[] roles = null;
            if (AuthenticationResult.Pass == this.authenticationProvider.Authenticate(username, pwd, out roles))
            {
                GenericPrincipal principal = new GenericPrincipal(new GenericIdentity(username), roles);
                Thread.CurrentPrincipal = principal;
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
