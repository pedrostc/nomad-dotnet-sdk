using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Nomad.DotNet.Model
{
    public class AclToken
    {
        public string AcessorId { get; set; }
        public string SecretId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IList<string> Policies => new List<string>();
        public bool Global { get; set; }
        public DateTime CreateTime { get; set; }
        public Int64 CreateIndex { get; set; }
        public Int64 ModifyIndex { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static AclPolicy FromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<AclPolicy>(json);
        }

        public static IList<AclPolicy> FromJsonArray(string json)
        {
            return JsonConvert.DeserializeObject<IList<AclPolicy>>(json);
        }
    }
}
