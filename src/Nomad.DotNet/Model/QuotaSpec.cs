using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Nomad.DotNet.Model
{
    public class QuotaSpec : ApiObject<QuotaSpec>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<QuotaLimit> Limits { get; set; } = new List<QuotaLimit>();
        public BigInteger CreateIndex { get; set; }
        public BigInteger ModifyIndex { get; set; }
    }
}
