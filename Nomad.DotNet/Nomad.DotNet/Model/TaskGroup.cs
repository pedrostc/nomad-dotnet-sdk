using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class TaskGroup : ApiObject<TaskGroup>
    {
        public string Name { get; set; }
        public int? Count { get; set; }
        public IList<Constraint> Constraints { get; set; } = new List<Constraint>();
        public IList<Task> Tasks { get; set; } = new List<Task>();
        public RestartPolicy RestartPolicy { get; set; }
        public EphemeralDisk EphemeralDisk { get; set; }
        public UpdateStrategy Update { get; set; }
        public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();
    }
}