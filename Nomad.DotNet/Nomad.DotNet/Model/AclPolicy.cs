using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nomad.DotNet.Model
{
    public class AclPolicy: ApiObject<AclPolicy>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
    }
}
