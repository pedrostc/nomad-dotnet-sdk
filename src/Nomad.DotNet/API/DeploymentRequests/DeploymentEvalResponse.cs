namespace Nomad.DotNet.API.DeploymentRequests
{
    public class DeploymentEvalResponse
    {
        public string EvalId { get; set; }
        public int EvalCreateIndex { get; set; }
        public int DeploymentModifyIndex { get; set; }
        public int RevertedJobVersion { get; set; }
        public int Index { get; set; }
    }
}
