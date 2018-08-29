using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class JobPlanResponse: ApiObject<JobPlanResponse>
    {
        public BigInteger JobModifyIndex { get; set; }
        public IList<Evaluation> CreatedEvals { get; set; } = new List<Evaluation>();
        public JobDiff Diff { get; set; }
        public PlanAnnotations Annotations { get; set; }
        public IDictionary<string, AllocationMetric> FailedTgAllocs { get; set; } = 
            new Dictionary<string, AllocationMetric>();
        public DateTime NextPeriodicLaunch { get; set; }
        public string Warnings { get; set; }
    }
}
