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
            Job job = await ProcessGetAsync<Job>(uri);

            return job;

        }
        public async Task<IList<Job>> List(string prefix = null)
        {
            Uri uri = buildUriForList(prefix);
            IList<Job> jobs = await ProcessGetAsync<List<Job>>(uri);

            return jobs;
        }

        public async Task<CreateResponse> Create(CreateRequest requestObj)
        {
            Uri uri = buildUriForCollection();
            CreateResponse response = await ProcessPostAsync<CreateResponse>(uri, requestObj);

            return response;
        }

        public async Task<Job> Parse(ParseRequest requestObj)
        {
            Uri uri = buildUriForCollection("parse");
            Job response = await ProcessPostAsync<Job>(uri, requestObj);

            return response;
        }

        public async Task<VersionsResponse> Versions(string id)
        {
            string methodName = "versions";
            Uri uri = buildUriForResourceId(id, methodName);
            VersionsResponse versions = await ProcessGetAsync<VersionsResponse>(uri);

            return versions;
        }

    }
}
