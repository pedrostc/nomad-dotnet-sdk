using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class NetworkResource
    {
        public string Device { get; set; }
        public string Cidr { get; set; }
        public string Ip { get; set; }
        public int MBits { get; set; }
        public IList<Port> ReservedPorts { get; set; } = new List<Port>();
        public IList<Port> DynamicPorts { get; set; } = new List<Port>();
    }
}