namespace Nomad.DotNet.API.JobRequests
{
    public class ParseRequest
    {
        public string JobHCL { get; set; }
        public bool Canonicalize { get; set; }
    }
}
