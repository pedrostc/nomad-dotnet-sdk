using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Nomad.DotNet.Model;
using Nomad.DotNet.UriUtilities;

namespace Nomad.DotNet.API
{
    public class AllocationsApi : NomadApi<Allocation>
    {
        protected override string resourceName => "allocation";
        protected override string collectionName => "allocations";

        public AllocationsApi(HttpClient httpClient, NomadApiConfig apiConfig) :
            base(httpClient, apiConfig)
        { }

        public async Task<Allocation> Read(string id)
        {
            Uri uri = buildUriForResourceId(id);
            Allocation allocation = await ProcessGetAsync<Allocation>(uri);

            return allocation;

        }
        public async Task<IList<Allocation>> List(string prefix = null)
        {
            Uri uri = buildUriForList(prefix);
            IList<Allocation> allocations = await ProcessGetAsync<List<Allocation>>(uri);

            return allocations;
        }

    }
}
