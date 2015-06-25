using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public static class AuditManager
    {

        public const int minPriority = 1;
        public const int maxPriority = 10;
        public const int defaultPriority = 5;

        private static IAuditor defaultAuditor;

        private static IAuditorFactory factory;
        public static IAuditorFactory Factory
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                factory = value;
                defaultAuditor = factory.Create();
            }
        }

        public static IAuditor GetAuditor(bool? singleton = true)
        {
            if (singleton.HasValue && singleton.Value)
            {
                return defaultAuditor;
            }
            else
            {
                return factory.Create();
            }
        }

    }

}
