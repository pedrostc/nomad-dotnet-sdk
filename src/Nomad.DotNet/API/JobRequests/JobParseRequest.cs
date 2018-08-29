namespace Nomad.DotNet.API.JobRequests
{
    public class JobParseRequest
    {
        public string JobHCL { get; set; }
        public bool Canonicalize { get; set; }
    }
}
