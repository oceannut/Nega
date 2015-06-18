using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public static class LogManager
    {

        private static ILoggerFactory factory = new ConsoleLoggerFactory();
        public static ILoggerFactory Factory
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                factory = value;
            }
        }

        public static ILogger GetLogger()
        {
            return factory.Create();
        }

    }

}
