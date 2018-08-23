using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Nomad.DotNet.Model
{
    public class Job : ApiObject<Job>
    {
        public bool Stop { get; set; }
        public string Region { get; set; }
        public string Namespace { get; set; }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Priority { get; set; }
        public bool AllAtOnce { get; set; }
        public IList<string> Datacenters { get; set; } = new List<string>();
        public IList<Constraint> Constraints { get; set; } = new List<Constraint>();
        public IList<TaskGroup> TaskGroups { get; set; } = new List<TaskGroup>();
        public UpdateStrategy Update { get; set; }
        public PeriodicConfig Periodic { get; set; }
        public ParameterizedJobConfig ParameterizedJob { get; set; }
        public byte[] Payload { get; set; }
        public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();
        public string VaultToken { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public bool Stable { get; set; }
        public BigInteger Version { get; set; }
        public long SubmitTime { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
        public BigInteger JobModifyIndex { get; set; }
    }
}
