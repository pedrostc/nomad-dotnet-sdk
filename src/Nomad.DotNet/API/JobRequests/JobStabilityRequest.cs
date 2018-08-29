namespace Nomad.DotNet.API.JobRequests
{
    public class JobStabilityRequest
    {
        public string JobId { get; set; }
        public int JobVersion { get; set; }
        public bool Stable { get; set; }
    }
}
