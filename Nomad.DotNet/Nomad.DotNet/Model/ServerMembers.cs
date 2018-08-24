using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class ServerMembers : ApiObject<ServerMembers>
    {
        public string ServerName { get; set; }
        public string ServerRegion { get; set; }
        public string ServerDc { get; set; }
        public IList<AgentMember> Members { get; set; } = 
            new List<AgentMember>();
    }
}
