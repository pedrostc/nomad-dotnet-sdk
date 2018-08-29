using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class Evaluation : ApiObject<Evaluation>
    {
        public string Id { get; set; }
        public int Priority { get; set; }
        public string Type { get; set; }
        public string TriggeredBy { get; set; }
        public string Namespace { get; set; }
        public string JobId { get; set; }
        public BigInteger JobModifyIndex{ get; set; }
        public string NodeId { get; set; }
        public BigInteger NodeModifyIndex { get; set; }
        public string DeploymentId { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public long Wait { get; set; }
        public string NextEval { get; set; }
        public string PreviousEval { get; set; }
        public string BlockedEval { get; set; }
        public IDictionary<string, AllocationMetric> FailedTgAllocs { get; set; } =
            new Dictionary<string, AllocationMetric>();
        public IDictionary<string, bool> ClassEligibility { get; set; } =
            new Dictionary<string, bool>();
        public bool EscapedComputedClass { get; set; }
        public string QuotaLimitReached { get; set; }
        public bool AnnotatePlan { get; set; }
        public IDictionary<string, int> QueuedAllocations { get; set; } =
            new Dictionary<string, int>();
        public BigInteger SnapshotIndex { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
