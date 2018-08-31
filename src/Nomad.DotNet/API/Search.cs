using Nomad.DotNet.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class Search : NomadApi
    {
        public Search(HttpClient httpClient, NomadApiConfig apiConfig) :
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "search";

        protected override string collectionName => throw new NotImplementedException();

        public async Task<SearchResponse> PerformSearch(string prefix, string context)
        {
            var requestObj = new { Prefix = prefix, Context = context };
            Uri uri = buildResourceUri();

            SearchResponse response = await ProcessPostAsync<SearchResponse>(uri, requestObj);

            return response;
        }
    }
}
