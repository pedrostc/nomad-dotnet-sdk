using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class JobSummary : ApiObject<JobSummary>
    {
        public string JobId { get; set; }
        public string Namespace { get; set; }
        public IDictionary<string, TaskGroupSummary> Summary { get; set; } = new Dictionary<string, TaskGroupSummary>();
        public JobChildrenSummary Children { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}