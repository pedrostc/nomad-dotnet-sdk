using Nomad.DotNet.Model;

namespace Nomad.DotNet.API.JobRequests
{
    public class JobUpdateRequest : JobCreateRequest
    {
        public JobUpdateRequest(Job job) : base(job) { }
    }
}
