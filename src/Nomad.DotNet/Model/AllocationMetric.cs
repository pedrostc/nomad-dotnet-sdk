using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class AllocationMetric: ApiObject<AllocationMetric>
    {
        public int NodesEvaluated { get; set; }
        public int NodesFiltered { get; set; }
        public IDictionary<string, int> NodesAvailable { get; set; } = new Dictionary<string, int>();
        public IDictionary<string, int> ClassFiltered { get; set; } = new Dictionary<string, int>();
        public IDictionary<string, int> ConstraintFiltered { get; set; } = new Dictionary<string, int>();
        public int NodesExhausted { get; set; }
        public IDictionary<string,int> ClassExhausted { get; set; } = new Dictionary<string, int>();
        public IDictionary<string, int> DimensionExhausted { get; set; } = new Dictionary<string, int>();
        public IList<string> QuotaExhausted { get; set; } = new List<string>();
        public IDictionary<string, double> Scores { get; set; } = new Dictionary<string, double>();
        public long AllocationTime { get; set; }
        public int CoalescedFailures { get; set; }
    }
}
