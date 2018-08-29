using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class SentinelPolicy : ApiObject<SentinelPolicy>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Scope { get; set; }
        public string EnforcementLevel { get; set; }
        public string Policy { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
