namespace Nomad.DotNet.Model
{
    public class AgentHealthResponse : ApiObject<AgentHealthResponse>
    {
        public AgentHealth Client { get; set; }
        public AgentHealth Server { get; set; }
    }
}
