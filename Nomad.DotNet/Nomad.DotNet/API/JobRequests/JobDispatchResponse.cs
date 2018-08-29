namespace Nomad.DotNet.API.JobRequests
{
    public class JobDispatchResponse
    {
        public int Index { get; set; }
        public int JobCreateIndex { get; set; }
        public int EvalCreateIndex { get; set; }
        public string EvalId { get; set; }
        public string DispatchedJobId { get; set; }
    }
}
