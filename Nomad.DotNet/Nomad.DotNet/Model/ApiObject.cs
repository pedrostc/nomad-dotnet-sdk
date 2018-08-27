using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nomad.DotNet.Model
{
    public abstract class ApiObject<T>
    {
        private IDictionary<string, object> UnmappedProperties { get; set; } = new Dictionary<string, object>();

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public static T FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static IList<T> FromJsonArray(string json)
        {
            return JsonConvert.DeserializeObject<IList<T>>(json);
        }
    }
}
