using System.Collections.Generic;

namespace Nomad.DotNet.API.DeploymentRequests
{
    public class DeploymentAllocationHealthRequest
    {
        public string DeploymentId { get; set; }
        public IList<string> HealthyAllocationIds { get; set; }
        public IList<string> UnhealthyAllocationIds { get; set; }
    }
}
