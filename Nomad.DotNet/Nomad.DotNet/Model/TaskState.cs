using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nomad.DotNet.Model
{
    public class TaskState: ApiObject<TaskState>
    {
        public string State { get; set; }
        public bool Failed { get; set; }
        public BigInteger Restarts { get; set; }
        public DateTime LastRestart { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public IList<TaskEvent> Events { get; set; } = new List<TaskEvent>();
    }
}
