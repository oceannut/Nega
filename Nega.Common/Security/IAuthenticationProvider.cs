using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public enum AuthenticationResult
    {
        Pass,
        UsernameRequired,
        PwdRequired,
        UsernameBadFormat,
        PwdBadFormat,
        NotExisted,
        Mismatch
    }


    public interface IAuthenticationProvider
    {

        AuthenticationResult Authenticate(string username, string pwd);

        AuthenticationResult Authenticate(string username, string pwd, out string[] roles);

    }

}
