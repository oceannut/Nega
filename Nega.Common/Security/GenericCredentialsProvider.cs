using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public class GenericCredentialsProvider : ICredentialsProvider
    {

        private Random random = new Random();
        private MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        public string GenerateUserToken(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException();
            }

            byte[] inBytes = Encoding.UTF8.GetBytes(string.Format("{0}{1}{2}", username, random.Next(), DateTime.Now));
            byte[] outBytes = md5.ComputeHash(inBytes);

            return BitConverter.ToString(outBytes).Replace("-", "");
        }

    }

}
