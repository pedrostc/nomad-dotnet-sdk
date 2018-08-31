using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class SystemMaintenance : NomadApi
    {
        public SystemMaintenance(HttpClient httpClient, NomadApiConfig apiConfig) : 
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "system";

        protected override string collectionName => throw new NotImplementedException();

        public async Task ForceGC()
        {
            Uri uri = buildResourceUri(method: "gc");
            await SendPutAsync(uri);
        }
        public async Task ReconcileSummaries()
        {
            Uri uri = buildResourceUri(method: "reconcile/summaries");
            await SendPutAsync(uri);
        }
    }
}
