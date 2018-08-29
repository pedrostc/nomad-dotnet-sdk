using System;
using System.Collections.Generic;
using System.Text;

namespace Nomad.DotNet.API.JobRequests
{
    public class JobEvaluateRequest
    {
        public string JobId { get; set; }
        public JobEvaluateOptions EvalOptions { get; set; }
    }

    public class JobEvaluateOptions
    {
        public bool ForceReschedule { get; set; }
    }
}
