using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public class GenericAuditor : IAuditor
    {

        private readonly IClientFinder clientFinder;
        private readonly IAuditWriter auditWriter;

        public GenericAuditor(IClientFinder clientFinder, IAuditWriter auditWriter)
        {
            this.clientFinder = clientFinder;
            this.auditWriter = auditWriter;
        }

        public void Audit(string resourceName, int resourceMethod, string content,
            OperationResult? result = OperationResult.Success,
            int? priority = AuditManager.DefaultPriority, 
            Func<DateTime> timestampFactory = null)
        {
            Audit(
                new Resource
                {
                    Name = resourceName,
                    Method = resourceMethod
                },
                content,
                result,
                priority,
                timestampFactory);
        }

        public void Audit(Resource resource, string content,
            OperationResult? result = OperationResult.Success,
            int? priority = AuditManager.DefaultPriority, 
            Func<DateTime> timestampFactory = null)
        {
            Audit(
                new AuditEntry
                {
                    Resource = resource,
                    Content = content,
                    Priority = priority.HasValue ? priority.Value : AuditManager.DefaultPriority,
                    Creation = timestampFactory == null ? DateTime.Now : timestampFactory()
                });
        }

        public void Audit(AuditEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException();
            }
            if (entry.Resource == null
                || string.IsNullOrWhiteSpace(entry.Resource.Name)
                || entry.Resource.Method < 1)
            {
                throw new ArgumentException("entry.Resource");
            }
            if (entry.Priority < AuditManager.MinPriority
                || entry.Priority > AuditManager.MaxPriority)
            {
                throw new ArgumentOutOfRangeException("entry.Priority");
            }

            if (string.IsNullOrWhiteSpace(entry.User))
            {
                Client client = this.clientFinder.OperationClient;
                if (client != null)
                {
                    if (!string.IsNullOrWhiteSpace(client.Username))
                    {
                        entry.User = client.Username;
                        entry.UserCategory = ThreadUserCategory.User;
                    }
                    else
                    {
                        entry.UserCategory = ThreadUserCategory.Service;
                    }
                    entry.Client = client.IP;
                }
                else
                {
                    entry.UserCategory = ThreadUserCategory.Service;
                }
            }
            else
            {
                entry.UserCategory = ThreadUserCategory.User;
            }

            if (entry.Creation == DateTime.MinValue)
            {
                entry.Creation = DateTime.Now;
            }

            this.auditWriter.Write(entry);
        }

    }

}
