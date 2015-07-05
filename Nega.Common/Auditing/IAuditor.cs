using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public interface IAuditor
    {

        void Audit(string resourceName, int resourceMethod, string content, Func<DateTime> timestampFactory = null);

        void Audit(string resourceName, int resourceMethod, string content, int priority, Func<DateTime> timestampFactory = null);

        void Audit(Resource resource, string content, Func<DateTime> timestampFactory = null);

        void Audit(Resource resource, string content, int priority, Func<DateTime> timestampFactory = null);

        void Audit(AuditEntry entry);

    }

}
