using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class JobListStub : ApiObject<JobListStub>
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Priority { get; set; }
        public bool Periodic { get; set; }
        public bool ParameterizedJob { get; set; }
        public bool Stop { get; set; }
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public JobSummary JobSummary { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
        public BigInteger JobModifyIndex { get; set; }
        public long SubmitTime { get; set; }
    }
}
