using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public interface IAuditor
    {

        void Audit(Resource resource, string content, Func<DateTime> timestampFactory = null);

        void Audit(Resource resource, string content, int priority, Func<DateTime> timestampFactory = null);

        void Audit(AuditEntry entry);

    }

}
