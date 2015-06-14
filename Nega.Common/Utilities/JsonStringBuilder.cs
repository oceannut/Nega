using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public class JsonStringBuilder
    {

        private StringBuilder buffer;

        public JsonStringBuilder()
        {
            buffer = new StringBuilder();
        }

        public JsonStringBuilder AppendLeftBrace()
        {
            buffer.Append("{");

            return this;
        }

        public JsonStringBuilder AppendRightBrace()
        {
            buffer.Append("}");

            return this;
        }

        public JsonStringBuilder AppendComma()
        {
            buffer.Append(",");

            return this;
        }

        public JsonStringBuilder Append(string key, object value)
        {
            AppendKey(key);
            buffer.Append(":");
            AppendValue(value);

            return this;
        }

        public JsonStringBuilder Append(string[] keys, object[] values)
        {
            if (keys == null || keys.Length == 0 
                || values == null || values.Length == 0 
                || keys.Length != values.Length)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < keys.Length; i++)
            {
                Append(keys[i], values[i]);
            }

            return this;
        }

        public JsonStringBuilder Append(IEnumerable<KeyValuePair<string, object>> items)
        {
            if (items == null || items.Count() == 0)
            {
                throw new ArgumentNullException();
            }

            foreach (var item in items)
            {
                Append(item.Key, item.Value);
            }

            return this;
        }

        public override string ToString()
        {
            return buffer.ToString();
        }

        private void AppendKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException();
            }

            buffer.Append("\"").Append(key).Append("\"");
        }

        private void AppendValue(object value)
        {
            if (value != null)
            {
                buffer.Append("\"").Append(value).Append("\"");
            }
            else
            {
                buffer.Append("\"\"");
            }
        }

    }

}
