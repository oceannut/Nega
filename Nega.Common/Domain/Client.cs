using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public class Client
    {

        public string Username { get; set; }

        public bool IsAuthenticated { get; set; }

        public string UserToken { get; set; }

        public string IP { get; set; }

        public string[] Roles
        {
            get 
            { 
                return new string[] { "admin" }; 
            }
        }

    }

}
