using System;
using System.Collections.Generic;
using System.Text;

namespace Nomad.DotNet.API.JobRequests
{
    public class JobRevertResponse
    {
        public string JobId { get; set; }
        public int JobVersion { get; set; }
        public int? EnforcePriorVersion { get; set; }
    }
}
