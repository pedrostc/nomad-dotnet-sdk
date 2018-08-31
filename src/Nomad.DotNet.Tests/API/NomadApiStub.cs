using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nomad.DotNet.API;

namespace Nomad.DotNet.Tests.API
{
    public class NomadApiStub : NomadApi
    {
        public NomadApiStub(HttpClient httpClient, NomadApiConfig apiConfig) : base(httpClient, apiConfig)
        { }

        protected override string resourceName => "path";

        protected override string collectionName => "path";

        public async Task<dynamic> GetWithType()
        {
            Uri uri = buildResourceUri();
            dynamic response = await ProcessGetAsync<dynamic>(uri);

            return response;
        }

        public async Task<string> GetText()
        {
            Uri uri = buildResourceUri();
            string response = await ProcessGetTextAsync(uri);

            return response;
        }
    }
}
