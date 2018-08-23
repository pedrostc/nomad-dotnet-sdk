using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class Deployment : ApiObject<Deployment>
    {
        public string Id { get; set; }
        public string Namespace { get; set; }
        public string JobId { get; set; }
        public BigInteger JobVersion { get; set; }
        public BigInteger JobModifyIndex { get; set; }
        public BigInteger JobCreateIndex { get; set; }
        public IDictionary<string, DeploymentState> TaskGoups { get; set; } = 
            new Dictionary<string, DeploymentState>();
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
