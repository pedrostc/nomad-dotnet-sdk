using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class NodeListStub : ApiObject<NodeListStub>
    {
        public string Id { get; set; }
        public string Datacenter { get; set; }
        public string Name { get; set; }
        public string NodeClass { get; set; }
        public string Version { get; set; }
        public bool Drain { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
