using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class PlanAnnotations : ApiObject<PlanAnnotations>
    {
        public IDictionary<string, DesiredUpdates> DesiredTgUpdates { get; set; } =
            new Dictionary<string, DesiredUpdates>();
    }
}