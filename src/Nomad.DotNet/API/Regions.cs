using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class Regions : NomadApi
    {
        public Regions(HttpClient httpClient, NomadApiConfig apiConfig) :
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => throw new NotImplementedException();

        protected override string collectionName => "regions";

        public async Task<IList<string>> List()
        {
            Uri uri = buildCollectionUri();
            IList<string> regions = await ProcessGetAsync<List<string>>(uri);

            return regions;
        }
    }
}
