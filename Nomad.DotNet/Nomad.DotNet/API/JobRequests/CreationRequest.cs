namespace Nomad.DotNet.API.JobRequests
{
    public class CreationRequest
    {
        public Model.Job Job { get; set; }
        public bool? EnforceIndex { get; set; }
        public int? JobModifyIndex { get; set; }
        public bool? PolicyOverride { get; set; }
    }
}
