using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class ParameterizedJobConfig : ApiObject<ParameterizedJobConfig>
    {
        public string Payload { get; set; }
        public IList<string> MetaRequired { get; set; } = new List<string>();
        public IList<string> MetaOptional { get; set; } = new List<string>();
    }
}