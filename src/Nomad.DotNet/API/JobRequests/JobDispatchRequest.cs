using System.Collections.Generic;

namespace Nomad.DotNet.API.JobRequests
{
    public class JobDispatchRequest
    {
        public string Payload { get; set; }
        public IDictionary<string, string> Meta { get; set; } = 
           new Dictionary<string, string>();
    }
}
