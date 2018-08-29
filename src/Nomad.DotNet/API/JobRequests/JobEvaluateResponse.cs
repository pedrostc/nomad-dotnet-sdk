using System;
using System.Collections.Generic;
using System.Text;

namespace Nomad.DotNet.API.JobRequests
{
    public class JobEvaluateResponse
    {
        public string EvalId { get; set; }
        public int EvalCreateIndex { get; set; }
        public int JobModifyIndex { get; set; }
    }
}
