using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class AgentSelf: ApiObject<AgentSelf>
    {
        public IDictionary<string, object> Config => new Dictionary<string, object>();
        public AgentMember Member { get; set; }
        public IDictionary<string, IDictionary<string, string>> Stats => new Dictionary<string, IDictionary<string, string>>();
    }
}
