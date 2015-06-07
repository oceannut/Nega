using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    [DataContract]
    public enum ResourceAccessMode
    {
        [EnumMember]
        Allowed,
        [EnumMember]
        Denied
    }

    [DataContract]
    public class ResourceAccess
    {

        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public ResourceAccessMode Mode { get; set; }

        [DataMember]
        public DateTime? Offset { get; set; }

        [DataMember]
        public int Duration { get; set; }

    }

}
