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

        public void Audit(string resourceName, int resourceMethod, string content, Func<DateTime> timestampFactory = null)
        {
            Audit(
                new Resource
                {
                    Name = resourceName,
                    Method = resourceMethod
                }, 
                content, 
                timestampFactory);
        }

        public void Audit(string resourceName, int resourceMethod, string content, int priority, Func<DateTime> timestampFactory = null)
        {
            Audit(
                new Resource
                {
                    Name = resourceName,
                    Method = resourceMethod
                },
                content,
                priority,
                timestampFactory);
        }

        public void Audit(Resource resource, string content, Func<DateTime> timestampFactory = null)
        {
            Audit(resource, content, AuditManager.defaultPriority, timestampFactory);
        }

        public void Audit(Resource resource, string content, int priority, Func<DateTime> timestampFactory = null)
        {
            Audit(
                new AuditEntry
                {
                    Resource = resource,
                    Content = content,
                    Priority = priority,
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
            if (entry.Priority < AuditManager.minPriority
                || entry.Priority > AuditManager.maxPriority)
            {
                throw new ArgumentOutOfRangeException("entry.Priority");
            }

            if (string.IsNullOrWhiteSpace(entry.User))
            {
                Client client = this.clientFinder.OperationClient;
                if (client != null)
                {
                    entry.User = client.Username;
                    entry.UserCategory = ThreadUserCategory.User;
                    entry.Client = client.IP;
                }
                else
                {
                    entry.UserCategory = ThreadUserCategory.Service;
                }
            }
            else
            {
                entry.UserCategory = ThreadUserCategory.Service;
            }

            if (entry.Creation == DateTime.MinValue)
            {
                entry.Creation = DateTime.Now;
            }

            this.auditWriter.Write(entry);
        }

    }

}
