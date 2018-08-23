using System.Collections.Generic;

namespace Nomad.DotNet.Model
{
    public class Vault : ApiObject<Vault>
    {
        public IList<string> Policies { get; set; } = new List<string>();
        public bool Env { get; set; }
        public string ChangeMode { get; set; }
        public string ChangeSignal { get; set; }
    }
}