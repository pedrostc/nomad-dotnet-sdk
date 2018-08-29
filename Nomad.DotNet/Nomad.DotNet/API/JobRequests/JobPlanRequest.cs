namespace Nomad.DotNet.API.JobRequests
{
    public class JobPlanRequest
    {
        public string JobId { get; set; }
        public bool Diff { get; set; }
        public bool PolicyOverride { get; set; }
    }
}
