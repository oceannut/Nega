using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Nega.WcfCommon
{

    public static class JsonHelper
    {

        public static string Serialize<T>(T obj, 
            DataContractJsonSerializer serializer = null, 
            Encoding encoding = null)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            DataContractJsonSerializer _serializer = serializer ?? new DataContractJsonSerializer(typeof(T));
            Encoding enc = encoding ?? Encoding.UTF8;
            using (MemoryStream stream = new MemoryStream())
            {
                _serializer.WriteObject(stream, obj);
                return enc.GetString(stream.ToArray());
            }
        }

        public static T Deserialize<T>(string s, 
            DataContractJsonSerializer serializer = null, 
            Encoding encoding = null)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentNullException();
            }

            DataContractJsonSerializer _serializer = serializer ?? new DataContractJsonSerializer(typeof(T));
            Encoding enc = encoding ?? Encoding.UTF8;
            using (MemoryStream stream = new MemoryStream(enc.GetBytes(s)))
            {
                return (T)_serializer.ReadObject(stream);
            }
        }

    }

}
