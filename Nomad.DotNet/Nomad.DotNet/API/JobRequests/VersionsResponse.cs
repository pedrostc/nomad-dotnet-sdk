using System.Collections.Generic;
using Nomad.DotNet.Model;

namespace Nomad.DotNet.API.JobRequests
{
    public class VersionsResponse
    {
        public List<Job> VSersions { get; set; }
        public List<JobDiff> JobDifferences { get; set; }
    }
}
