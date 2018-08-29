using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class TaskDiff : ApiObject<TaskDiff>
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public IList<FieldDiff> Fields { get; set; } = new List<FieldDiff>();
        public IList<ObjectDiff> Objects { get; set; } = new List<ObjectDiff>();
        public IList<string> Annotations { get; set; } = new List<string>();
    }
}