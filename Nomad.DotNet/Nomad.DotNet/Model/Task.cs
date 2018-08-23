using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class Task : ApiObject<Task>
    {
        public string Name { get; set; }
        public string Driver { get; set; }
        public string User { get; set; }
        public IDictionary<string, object> Config { get; set; } = new Dictionary<string, object>();
        public IList<Constraint> Constraints { get; set; } = new List<Constraint>();
        public IDictionary<string, string> Env { get; set; } = new Dictionary<string, string>();
        public IList<Service> Services { get; set; } = new List<Service>();
        public Resources Resources { get; set; }
        public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();
        public long KillTimeout { get; set; }
        public LogConfig LogConfig { get; set; }
        public IList<TaskArtifact> Artifacts { get; set; } = new List<TaskArtifact>();
        public Vault Vault { get; set; }
        public IList<Template> Templates { get; set; } = new List<Template>();
        public DispatchPayloadConfig DispatchPayload { get; set; }
        public bool Leader { get; set; }
        public long ShutdownDelay { get; set; }
    }
}