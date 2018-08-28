using Nomad.DotNet.Model;

namespace Nomad.DotNet.API.JobRequests
{
    public class CreationRequest
    {
        public Job Job { get; set; }
        public bool? EnforceIndex { get; set; }
        public int? JobModifyIndex { get; set; }
        public bool? PolicyOverride { get; set; }

        public CreationRequest(Job job)
        {
            Job = job;
        }
    }
}
