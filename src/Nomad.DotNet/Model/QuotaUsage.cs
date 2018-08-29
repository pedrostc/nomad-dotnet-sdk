using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Nomad.DotNet.Model
{
    public class QuotaUsage : ApiObject<QuotaUsage>
    {
        public string Name { get; set; }
        public IDictionary<string, QuotaLimit> Used { get; set; } =
            new Dictionary<string, QuotaLimit>();
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
