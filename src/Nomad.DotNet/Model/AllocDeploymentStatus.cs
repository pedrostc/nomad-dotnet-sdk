using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class AllocDeploymentStatus: ApiObject<AllocDeploymentStatus>
    {
        public bool Healthy { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
