using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nomad.DotNet.Model
{
    public class AclPolicy
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
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
