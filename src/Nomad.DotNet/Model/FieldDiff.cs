using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class FieldDiff : ApiObject<FieldDiff>
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Old { get; set; }
        public string New { get; set; }
        public IList<string> Annotations { get; set; } = new List<string>();
    }
}
