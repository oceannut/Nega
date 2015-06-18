using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public class ConsoleLoggerFactory : ILoggerFactory
    {

        private static readonly ConsoleLogger _default = new ConsoleLogger();

        public ILogger Create()
        {
            return _default;
        }

    }

}
