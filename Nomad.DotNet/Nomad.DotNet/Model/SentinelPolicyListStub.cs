using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class SentinelPolicyListStub : ApiObject<SentinelPolicyListStub>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Scope { get; set; }
        public string EnforcementLevel { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
