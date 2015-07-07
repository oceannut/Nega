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
        public int Method { get; set; }

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

        public ResourceAttribute(string name, int method)
            : this(true)
        {
            if (string.IsNullOrWhiteSpace(name) || method < 1)
            {
                throw new ArgumentException();
            }

            this.Name = name;
            this.Method = method;
        }

    }

}
