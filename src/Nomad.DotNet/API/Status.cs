using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class Status : NomadApi
    {
        public Status(HttpClient httpClient, NomadApiConfig apiConfig) :
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "status";

        protected override string collectionName => throw new NotImplementedException();

        public async Task<string> Leader()
        {
            Uri uri = buildResourceUri(method: "leader");
            string leader = await ProcessGetTextAsync(uri);

            return leader;
        }

        public async Task<IList<string>> Peers()
        {
            Uri uri = buildResourceUri(method: "peers");
            IList<string> peers = await ProcessGetAsync<IList<string>>(uri);

            return peers;
        }
    }
}
