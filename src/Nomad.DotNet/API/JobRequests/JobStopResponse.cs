namespace Nomad.DotNet.API.JobRequests
{
    public class JobStopResponse
    {
        public string EvalId { get; set; }
        public int EvalCreateIndex { get; set; }
        public int JobModifyIndex { get; set; }
    }
}
