using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class AgentMember: ApiObject<AgentMember>
    {
        public string Name { get; set; }
        public string Addr { get; set; }
        public int Port { get; set; }
        public IDictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();
        public string Status { get; set; }
        public byte ProtocolMin { get; set; }
        public byte ProtocolMax { get; set; }
        public byte ProtocolCur { get; set; }
        public byte DelegateMin { get; set; }
        public byte DelegateMax { get; set; }
        public byte DelegateCur { get; set; }
    }
}
