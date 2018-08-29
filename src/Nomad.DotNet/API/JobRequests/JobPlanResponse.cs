using System;
using System.Collections.Generic;

using Nomad.DotNet.Model;

namespace Nomad.DotNet.API.JobRequests
{
    public class JobPlanResponse
    {
        public int JobModifyIndex { get; set; }
        public IList<Evaluation> CreatedEvals { get; set; } = new List<Evaluation>();
        public JobDiff Diff { get; set; }
        public PlanAnnotations Annotations { get; set; }
        public IDictionary<string, AllocationMetric> FailedTgAllocs { get; set; } =
            new Dictionary<string, AllocationMetric>();
        public DateTime NextPeriodicLaunch { get; set; }
        public string Warnings { get; set; }
    }
}
