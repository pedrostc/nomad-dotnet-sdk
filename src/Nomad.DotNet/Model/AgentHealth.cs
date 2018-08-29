namespace Nomad.DotNet.Model
{
    public class AgentHealth: ApiObject<AgentHealth>
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
    }
}
