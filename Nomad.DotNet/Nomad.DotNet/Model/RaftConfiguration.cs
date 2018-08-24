using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class RaftConfiguration : ApiObject<RaftConfiguration>
    {
        public IList<RaftServer> Servers { get; set; } = 
            new List<RaftServer>();
        public BigInteger Index { get; set; }
    }
}
