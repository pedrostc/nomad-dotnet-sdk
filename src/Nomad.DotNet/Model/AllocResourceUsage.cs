using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class AllocResourceUsage : ApiObject<AllocResourceUsage>
    {
        public ResourceUsage ResourceUsage { get; set; }
        public IDictionary<string, TaskResourceUsage> Tasks { get; set; }
        public long Timestamp { get; set; }
    }
}
