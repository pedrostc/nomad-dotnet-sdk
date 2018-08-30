using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nomad.DotNet.UriUtilities
{
    public class QueryStringBuilder
    {
        private IList<KeyValuePair<string, string>> fields;

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
            fields = new List<KeyValuePair<string, string>>();
        }

        public void AddField(string name, string value)
        {
            fields.Add(new KeyValuePair<string, string>(name, value));
        }
        public void RemoveField(string name)
        {
            var items = fields.Where(item => item.Key == name).ToList();
            items.ForEach(item => fields.Remove(item));
        }
        public bool ContainsField(string name)
        {
            return fields.Any(item => item.Key == name);
        }
        public void Clear()
        {
            fields.Clear();
        }
    }
}
