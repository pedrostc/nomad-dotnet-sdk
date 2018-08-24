using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class MemoryStats : ApiObject<MemoryStats>
    {
        public BigInteger Rss { get; set; }
        public BigInteger Cache { get; set; }
        public BigInteger Swap { get; set; }
        public BigInteger MaxUsage { get; set; }
        public BigInteger KernelUsage { get; set; }
        public BigInteger KernelMaxUsage { get; set; }
        public IList<string> Measured { get; set; } = new List<string>();
    }
}
