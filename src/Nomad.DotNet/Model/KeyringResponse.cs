using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class KeyringResponse : ApiObject<KeyringResponse>
    {
        public IDictionary<string, string> Messages { get; set; } = 
            new Dictionary<string, string>();
        public IDictionary<string, int> Keys { get; set; } = 
            new Dictionary<string, int>();
        public int NumNodes { get; set; }
    }
}
