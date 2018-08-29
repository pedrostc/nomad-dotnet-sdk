namespace Nomad.DotNet.API.JobRequests
{
    public class JobCreateResponse
    {
        public string EvalId { get; set; }
        public int EvalCreateIndex { get; set; }
        public int JobModifyIndex { get; set; }
        public string Warnings { get; set; }
        public int Index { get; set; }
        public int LastContact { get; set; }
        public bool KnownLeader { get; set; }
    }
}
