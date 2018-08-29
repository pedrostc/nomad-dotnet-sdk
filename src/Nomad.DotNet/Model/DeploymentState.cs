using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class DeploymentState : ApiObject<DeploymentState>
    {
        public IList<string> PlacedCanaries { get; set; } = new List<string>();
        public bool AutoRevert { get; set; }
        public bool Promoted { get; set; }
        public int DesiredCanaries { get; set; }
        public int DesiredTotal { get; set; }
        public int PlacedAllocs { get; set; }
        public int HealthyAllocs { get; set; }
        public int UnhealthyAllocs { get; set; }
    }
}