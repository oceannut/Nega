using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public interface IPrincipalProvider
    {

        IPrincipal Principal { get; }

        bool Authenticate(string username, string pwd);

    }

}
