using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class JobDispatchResponse : ApiObject<JobDispatchResponse>
    {
        public string DispatchedJobId { get; set; }
        public string EvalId { get; set; }
        public BigInteger EvalCreateIndex { get; set; }
        public BigInteger JobCreateIndex { get; set; }
    }
}
