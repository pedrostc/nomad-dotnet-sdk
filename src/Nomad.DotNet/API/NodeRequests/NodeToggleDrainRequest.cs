namespace Nomad.DotNet.API.NodeRequests
{
    public class NodeToggleDrainRequest
    {
        public DrainSpec DrainSpec { get; set; }
        public bool MarkEligible { get; set; }
    }

    public class DrainSpec
    {
        public int Deadline { get; set; }
        public bool IgnoreSystemJobs { get; set; }
    }
}
