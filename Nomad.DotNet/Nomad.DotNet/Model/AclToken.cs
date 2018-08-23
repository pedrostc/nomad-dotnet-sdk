using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Nomad.DotNet.Model
{
    public class AclToken : ApiObject<AclPolicy>
    {
        public string AcessorId { get; set; }
        public string SecretId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IList<string> Policies => new List<string>();
        public bool Global { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
