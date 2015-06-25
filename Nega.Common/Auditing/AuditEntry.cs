using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    [DataContract]
    public class AuditEntry
    {

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public Resource Resource { get; set; }

        [DataMember]
        public string User { get; set; }

        [DataMember]
        public ThreadUserCategory UserCategory { get; set; }

        [DataMember]
        public string Client { get; set; }

        [DataMember]
        public int Priority { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public DateTime Creation { get; set; }

    }

}
