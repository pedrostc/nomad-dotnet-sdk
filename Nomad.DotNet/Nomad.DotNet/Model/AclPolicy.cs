using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class AclPolicy: ApiObject<AclPolicy>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
