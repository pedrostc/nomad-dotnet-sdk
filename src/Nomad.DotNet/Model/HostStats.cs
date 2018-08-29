using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Nomad.DotNet.Model
{
    public class HostStats : ApiObject<HostStats>
    {
        public HostMemoryStats Memory { get; set; }
        public IList<HostCpuStats> Cpu { get; set; } = new List<HostCpuStats>();
        public IList<HostDiskStats> DiskStats { get; set; } = new List<HostDiskStats>();
        public BigInteger Uptime { get; set; }
        public double CpuTicksConsumed { get; set; }
    }
}
