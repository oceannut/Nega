using System;
using System.Runtime.Serialization;

namespace Nega.Common
{

    [DataContract]
    public enum ThreadUserCategory
    {
        [EnumMember]
        User,
        [EnumMember]
        Service
    }

}