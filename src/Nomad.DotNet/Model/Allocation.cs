using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class Allocation: ApiObject<Allocation>
    {
        public string Id { get; set; }
        public string Namespace { get; set; }
        public string EvalId { get; set; }
        public string Name { get; set; }
        public string NodeId { get; set; }
        public string JobId { get; set; }
        public Job Job { get; set; }
        public string TaskGroup { get; set; }
        public Resources Resources { get; set; }
        public IDictionary<string, Resources> TaskResources { get; set; } = 
            new Dictionary<string, Resources>();
        public IDictionary<string, string> Services { get; set; } = 
            new Dictionary<string, string>();
        public AllocationMetric Metrics { get; set; }
        public string DesiredStatus { get; set; }
        public string DesiredDescription { get; set; }
        public string ClientStatus { get; set; }
        public string ClientDescription { get; set; }
        public IDictionary<string, TaskState> TaskStates { get; set; } = 
            new Dictionary<string, TaskState>();
        public string DeploymentId { get; set; }
        public AllocDeploymentStatus DeploymentStatus { get; set; }
        public string PreviousAllocation { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
        public BigInteger AllocModifyIndex { get; set; }
        public long CreateTime { get; set; }
    }
}
