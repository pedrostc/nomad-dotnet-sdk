using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class Node : ApiObject<Node>
    {
        public string Id { get; set; }
        public string Datacenter { get; set; }
        public string Name { get; set; }
        public string HttpAddr { get; set; }
        public bool TlsEnabled { get; set; }
        public IDictionary<string, string> Attributes { get; set; } = 
            new Dictionary<string, string>();
        public Resources Resources { get; set; }
        public Resources Reserved { get; set; }
        public IDictionary<string, string> Links { get; set; } =
            new Dictionary<string, string>();
        public IDictionary<string, string> Meta { get; set; } =
            new Dictionary<string, string>();
        public string NodeClass { get; set; }
        public bool Drain { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public long StatusUpdatedAt { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
