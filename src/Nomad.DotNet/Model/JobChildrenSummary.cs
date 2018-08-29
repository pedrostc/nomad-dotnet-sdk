using System;
using System.Collections.Generic;
using System.Text;

namespace Nomad.DotNet.Model
{
    public class JobChildrenSummary : ApiObject<JobChildrenSummary>
    {
        public long Pending { get; set; }
        public long Running { get; set; }
        public long Dead { get; set; }
    }
}
