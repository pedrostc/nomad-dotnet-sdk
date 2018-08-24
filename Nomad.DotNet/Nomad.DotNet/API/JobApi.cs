using Nomad.DotNet.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Nomad.DotNet.API
{
    public class JobApi : NomadApi<Job>
    {
        public JobApi(HttpClient httpClient) : base(httpClient)
        {
        }

        protected override string resourceName => "job";

    }
}
