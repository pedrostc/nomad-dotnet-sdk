using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class SearchResponse : ApiObject<SearchResponse>
    {
        public IDictionary<string, IList<string>> Matches { get; set; } =
            new Dictionary<string, IList<string>>();
        public IDictionary<string, bool> Truncations { get; set; } =
            new Dictionary<string, bool>();
    }
}
