using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class DeploymentUpdateResponse : ApiObject<DeploymentUpdateResponse>
    {
        public string EvalId { get; set; }
        public BigInteger EvalCreateIndex { get; set; }
        public BigInteger DeploymentModifyIndex { get; set; }
        public BigInteger RevertedJobVersion { get; set; }
    }
}
