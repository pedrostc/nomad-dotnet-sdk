namespace Nomad.DotNet.API.DeploymentRequests
{
    public class DeploymentPauseRequest
    {
        public string DeploymentId { get; set; }
        public bool Pause { get; set; }
    }
}
