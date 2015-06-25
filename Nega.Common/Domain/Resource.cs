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

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Method { get; set; }

    }

}
