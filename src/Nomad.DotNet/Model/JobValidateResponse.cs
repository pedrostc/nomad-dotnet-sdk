using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class JobValidateResponse : ApiObject<JobValidateResponse>
    {
        public bool DriverConfigValidated { get; set; }
        public IList<string> ValidationErrors { get; set; } = new List<string>();
        public string Error { get; set; }
        public string Warnings { get; set; }
    }
}
