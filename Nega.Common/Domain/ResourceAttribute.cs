using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    [DataContract]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class ResourceAttribute : Attribute
    {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Method { get; set; }

        [DataMember]
        public bool CheckAccess { get; set; }

        public ResourceAttribute()
        {
        }

        public ResourceAttribute(bool checkAccess)
        {
            this.CheckAccess = checkAccess;
        }

        public ResourceAttribute(string name)
            : this(true)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }

            this.Name = name;
        }

        public ResourceAttribute(string name, string method)
            : this(true)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentNullException();
            }

            this.Name = name;
            this.Method = method;
        }

    }

}
