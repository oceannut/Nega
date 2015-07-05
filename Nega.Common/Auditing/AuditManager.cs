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

        private static IAuditor auditor;
        public static IAuditor Auditor
        {
            get
            {
                if (auditor == null)
                {
                    throw new InvalidOperationException();
                }
                return auditor;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                auditor = value;
            }
        }

    }

}
