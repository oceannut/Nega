using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public interface IAuditWriter
    {

        void Write(AuditEntry entry);

        void Write(IEnumerable<AuditEntry> entries);

    }

}
