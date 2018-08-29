using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class AclToken : ApiObject<AclPolicy>
    {
        public string AcessorId { get; set; }
        public string SecretId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IList<string> Policies { get; set; } = new List<string>();
        public bool Global { get; set; }
        public DateTime CreateTime { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
