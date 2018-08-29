using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class ServiceCheck : ApiObject<ServiceCheck>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Command { get; set; }
        public IList<string> Args { get; set; } = new List<string>();
        public string Path { get; set; }
        public string Protocol { get; set; }
        public string PortLabel { get; set; }
        public long Interval { get; set; }
        public long Timeout { get; set; }
        public string InitialStatus { get; set; }
        public bool TlsSkipVerify { get; set; }
        public IDictionary<string, IList<string>> Header { get; set; } = new Dictionary<string, IList<string>>();
        public string Method { get; set; }
        public CheckRestart CheckRestart { get; set; }
    }
}