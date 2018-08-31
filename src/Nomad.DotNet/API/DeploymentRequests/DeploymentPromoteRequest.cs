using System.Collections.Generic;

namespace Nomad.DotNet.API.DeploymentRequests
{
    public class DeploymentPromoteRequest
    {
        public string DeploymentId { get; set; }
        public bool All { get; set; }
        public IList<string> Groups { get; set; }
    }
}
