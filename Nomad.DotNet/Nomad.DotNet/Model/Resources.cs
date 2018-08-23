using System;
using System.Collections.Generic;
using System.Text;

namespace Nomad.DotNet.Model
{
    public class Resources: ApiObject<Resources>
    {
        public int Cpu { get; set; }
        public int MemoryMb { get; set; }
        public int DiskMb { get; set; }
        public int Iops { get; set; }
        public IList<NetworkResource> Networks { get; set; } = new List<NetworkResource>();
    }
}
