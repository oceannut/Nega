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

        string Identity { get; }

        bool Authenticate(string client, string username, string pwd);

    }

}
