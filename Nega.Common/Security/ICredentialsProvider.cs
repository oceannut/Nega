using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public interface ICredentialsProvider
    {

        string GenerateUserToken(string username);

    }

}
