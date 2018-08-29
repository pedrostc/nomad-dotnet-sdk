using System.Collections.Generic;
using Nomad.DotNet.Model;

namespace Nomad.DotNet.API.JobRequests
{
    public class JobVersionsResponse
    {
        public List<Job> Versions { get; set; }
        public List<JobDiff> Diffs { get; set; }
        public int Index { get; set; }
    }
}
