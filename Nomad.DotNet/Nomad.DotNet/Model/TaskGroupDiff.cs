using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class TaskGroupDiff : ApiObject<TaskGroupDiff>
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public IList<FieldDiff> Fields { get; set; } = new List<FieldDiff>();
        public IList<ObjectDiff> Objects { get; set; } = new List<ObjectDiff>();
        public IList<TaskDiff> Tasks { get; set; } = new List<TaskDiff>();
        public IDictionary<string, BigInteger> Updates { get; set; } = 
            new Dictionary<string, BigInteger>();
    }
}