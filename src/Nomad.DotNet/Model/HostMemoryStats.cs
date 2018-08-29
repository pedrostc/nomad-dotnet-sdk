using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class HostMemoryStats : ApiObject<HostMemoryStats>
    {
        public BigInteger Total { get; set; }
        public BigInteger Available { get; set; }
        public BigInteger Used { get; set; }
        public BigInteger Free { get; set; }
    }
}
