using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class JobStabilityResponse : ApiObject<JobStabilityResponse>
    {
        public BigInteger JobModifyIndex { get; set; }
    }
}
