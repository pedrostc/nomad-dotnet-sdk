using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class HostDiskStats : ApiObject<HostDiskStats>
    {
        public string Device { get; set; }
        public string Mountpoint { get; set; }
        public BigInteger Size { get; set; }
        public BigInteger Used { get; set; }
        public BigInteger Available { get; set; }
        public double UsedPercent { get; set; }
        public double InodesUsedPercent { get; set; }
    }
}
