using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class TaskArtifact : ApiObject<TaskArtifact>
    {
        public string GetterSource { get; set; }
        public IDictionary<string, string> GetterOptions { get; set; } = new Dictionary<string, string>();
        public string GetterMode { get; set; }
        public string RelativeDest { get; set; }
    }
}