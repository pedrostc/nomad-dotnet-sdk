using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class AllocationListStub : ApiObject<AllocationListStub>
    {
        public string Id { get; set; }
        public string EvalId { get; set; }
        public string Name { get; set; }
        public string NodeId { get; set; }
        public string JobId { get; set; }
        public BigInteger JobVersion { get; set; }
        public string TaskGroup { get; set; }
        public string DesiredStatus { get; set; }
        public string DesiredDescription { get; set; }
        public string ClientStatus { get; set; }
        public string ClientDescription { get; set; }
        public IDictionary<string, TaskState> TaskStates { get; set; } = new Dictionary<string, TaskState>();
        public AllocDeploymentStatus DeploymentStatus { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
        public long CreateTime { get; set; }
    }
}
