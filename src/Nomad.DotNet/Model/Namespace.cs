using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class Namespace : ApiObject<Namespace>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quota { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
