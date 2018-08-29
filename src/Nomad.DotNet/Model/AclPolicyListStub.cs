using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class AclPolicyListStub : ApiObject<AclPolicyListStub>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
