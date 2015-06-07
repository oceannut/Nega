using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    [DataContract]
    public class Resource
    {

        public const string METHOD_SAVE = "save";
        public const string METHOD_UPDATE = "update";
        public const string METHOD_GET = "get";
        public const string METHOD_DELETE = "delete";
        public const string METHOD_LIST = "list";

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Method { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Module { get; set; }

    }

}
