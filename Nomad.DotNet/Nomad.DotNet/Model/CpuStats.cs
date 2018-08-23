using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class CpuStats : ApiObject<CpuStats>
    {
        public double SystemMode { get; set; }
        public double UserMode { get; set; }
        public double TotalTicks { get; set; }
        public BigInteger ThrottledPeriods { get; set; }
        public BigInteger ThrottledTime { get; set; }
        public double Percent { get; set; }
        public IList<string> Measured { get; set; } = new List<string>();
    }
}
