using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class JobDiff : ApiObject<JobDiff>
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public IList<FieldDiff> Fields { get; set; } = new List<FieldDiff>();
        public IList<ObjectDiff> Objects { get; set; } = new List<ObjectDiff>();
        public IList<TaskGroupDiff> TaskGroups { get; set; } = new List<TaskGroupDiff>();
    }
}
