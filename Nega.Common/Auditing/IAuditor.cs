using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public interface IAuditor
    {

        void Audit(string resourceName, int resourceMethod, string content,
            OperationResult? result = OperationResult.Success,
            int? priority = AuditManager.DefaultPriority,
            Func<DateTime> timestampFactory = null);

        void Audit(Resource resource, string content, 
            OperationResult? result = OperationResult.Success,
            int? priority = AuditManager.DefaultPriority, 
            Func<DateTime> timestampFactory = null);

        void Audit(AuditEntry entry);

    }

}
