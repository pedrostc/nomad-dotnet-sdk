using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class DesiredUpdates : ApiObject<DesiredUpdates>
    {
        public BigInteger Ignore { get; set; }
        public BigInteger Place { get; set; }
        public BigInteger Migrate { get; set; }
        public BigInteger Stop { get; set; }
        public BigInteger InPlaceUpdate { get; set; }
        public BigInteger DestructiveUpdate { get; set; }
        public BigInteger Canary { get; set; }
    }
}
