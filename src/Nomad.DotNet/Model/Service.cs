using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class Service : ApiObject<Service>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<string> Tags { get; set; } = new List<string>();
        public string PortLabel { get; set; }
        public string AddressMode { get; set; }
        public List<ServiceCheck> Checks { get; set; } = new List<ServiceCheck>();
        public CheckRestart CheckRestart { get; set; }
    }
}