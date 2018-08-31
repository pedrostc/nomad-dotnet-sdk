using Nomad.DotNet.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class Validate : NomadApi
    {
        public Validate(HttpClient httpClient, NomadApiConfig apiConfig) :
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "validate";

        protected override string collectionName => throw new NotImplementedException();

        public async Task<JobValidateResponse> Job(Job job)
        {
            Uri uri = buildResourceUri(method: "job");
            JobValidateResponse response = 
                await ProcessPostAsync<JobValidateResponse>(uri, job);

            return response;
        }
    }
}
