using System;
using System.Collections.Generic;
using System.Text;

namespace Nomad.DotNet.API.AgentRequests
{
    public class AgentJoinResponse
    {
        public string Error { get; set; }
        public int num_joined { get; set; }
    }
}
