using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Nomad.DotNet.API.JobRequests;
using Nomad.DotNet.Model;
using Nomad.DotNet.UriUtilities;

namespace Nomad.DotNet.API
{
    public class JobApi : NomadApi<Job>
    {
        private const string PREFIX_QUERY_FIELD = "prefix";
        protected override string resourceName => "job";
        protected override string collectionName => "jobs";
        public JobApi(HttpClient httpClient, NomadApiConfig apiConfig) : base(httpClient, apiConfig)
        { }

        private Uri buildUriForList(string prefix)
        {
            Uri uri = buildUriForCollection();

            if(!string.IsNullOrEmpty(prefix))
                uri = appendPrefixToUri(uri, prefix);

            return uri;
        }
        private Uri appendPrefixToUri(Uri uri, string prefix)
        {
            BetterUriBuilder builder = new BetterUriBuilder(uri);
            builder.AddQueryField(PREFIX_QUERY_FIELD, prefix);

            return builder.Uri;
        } 

        public async Task<Job> Read(string id)
        {
            Uri uri = buildUriForResourceId(id);

            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleReponseError(response);

            Job job = await response.Content.ReadAsAsync<Job>();
            return job;

        }
        public async Task<IList<Job>> List(string prefix = null)
        {
            Uri uri = buildUriForList(prefix);

            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleReponseError(response);

            IList<Job> jobs = await response.Content.ReadAsAsync<List<Job>>();
            return jobs;
        }

        public async Task<CreationResponse> Create(CreationRequest requestObj)
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(buildUriForCollection(), requestObj);

            await HandleReponseError(responseMessage);

            CreationResponse response = await responseMessage.Content.ReadAsAsync<CreationResponse>();
            return response;
        }
    }
}
