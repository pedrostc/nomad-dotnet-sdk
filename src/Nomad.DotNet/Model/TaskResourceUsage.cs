using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class TaskResourceUsage : ApiObject<TaskResourceUsage>
    {
        public ResourceUsage ResourceUsage { get; set; }
        public long Timestamp { get; set; }
        public IDictionary<string, ResourceUsage> Pids { get; set; } =
           new Dictionary<string,ResourceUsage> ();
    }
}