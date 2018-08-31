using System.Collections.Generic;
using System.Collections.Specialized;
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
        public QueryStringBuilder(string baseQuery): this()
        {
            initializeFields(baseQuery);
        }

        private void initializeFields(string baseQuery)
        {
            NameValueCollection baseFields = HttpUtility.ParseQueryString(baseQuery);
            baseFields.AllKeys
                .SelectMany(baseFields.GetValues, (key, value) => new { key, value })
                .ToList()
                .ForEach(item => AddField(item.key, item.value));
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
