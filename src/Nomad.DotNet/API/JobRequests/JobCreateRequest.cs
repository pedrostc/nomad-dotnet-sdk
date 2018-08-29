
using Nomad.DotNet.Model;

namespace Nomad.DotNet.API.JobRequests
{
    public class JobCreateRequest
    {
        public Job Job { get; set; }
        public bool? EnforceIndex { get; set; }
        public int? JobModifyIndex { get; set; }
        public bool? PolicyOverride { get; set; }

        public JobCreateRequest(Job job)
        {
            Job = job;
        }
    }
}
