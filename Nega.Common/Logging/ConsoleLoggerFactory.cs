using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public class ConsoleLoggerFactory : ILoggerFactory
    {

        private static readonly ConsoleLogger Default = new ConsoleLogger();

        private readonly Dictionary<Type, ILogger> dict;

        public ConsoleLoggerFactory()
        {
            this.dict = new Dictionary<Type, ILogger>();
        }

        public ILogger Create(Type type)
        {
            if (type == null)
            {
                return Default;
            }
            else
            {
                ILogger logger = null;
                lock (dict)
                {
                    dict.TryGetValue(type, out logger);
                    if (logger == null)
                    {
                        logger = new ConsoleLogger(type);
                        dict.Add(type, logger);
                    }
                }
                return logger;
            }
        }

    }

}
