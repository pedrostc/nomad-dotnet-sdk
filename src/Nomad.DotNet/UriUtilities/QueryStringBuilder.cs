using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomad.DotNet.UriUtilities
{
    public class QueryStringBuilder
    {
        private IDictionary<string, string> fields;

        public string QueryString
        {
            get
            {
                if (fields.Count > 0)
                    return fields
                        .Select(keyValPair => $"{ HttpUtility.UrlEncode(keyValPair.Key)}={ HttpUtility.UrlEncode(keyValPair.Value)}")
                        .Aggregate((workingString, nextField) => $"{workingString}&{nextField}");
                else
                    return string.Empty;
            }
        }

        public QueryStringBuilder()
        {
            fields = new Dictionary<string, string>();
        }

        public void AddField(string name, string value)
        {
            fields.Add(name, value);
        }
        public void UpdateField(string name, string value)
        {
            fields[name] = value;
        }
        public void RemoveField(string name)
        {
            fields.Remove(name);
        }
        public bool ContainsField(string name)
        {
            return fields.ContainsKey(name);
        }
        public void Clear()
        {
            fields.Clear();
        }
    }
}
