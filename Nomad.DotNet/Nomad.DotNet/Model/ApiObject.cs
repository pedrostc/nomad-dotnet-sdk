using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nomad.DotNet.Model
{
    public abstract class ApiObject<T>
    {
        private IDictionary<string, object> UnmappedProperties => new Dictionary<string, object>();
        public Int64 CreateIndex { get; set; }
        public Int64 ModifyIndex { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
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
