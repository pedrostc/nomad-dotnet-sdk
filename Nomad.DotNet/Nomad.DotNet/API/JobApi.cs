using Nomad.DotNet.API.JobRequests;
using Nomad.DotNet.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class JobApi : NomadApi<Job>
    {
        protected override string resourceName => "job";
        protected override string collectionName => "jobs";
        public JobApi(HttpClient httpClient, Uri baseUri) : base(httpClient, baseUri)
        { }

        public async Task<CreationResponse> CreateAsync(CreationRequest requestObj)
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(buildUriForCollection(), requestObj);
            CreationResponse response = await responseMessage.Content.ReadAsAsync<CreationResponse>();

            return response;
        }
    }
}
