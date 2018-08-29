using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Nomad.DotNet.Model;
using Nomad.DotNet.UriUtilities;

namespace Nomad.DotNet.API
{
    public class AllocationApi : NomadApi<Allocation>
    {
        private const string PREFIX_QUERY_FIELD = "prefix";
        protected override string resourceName => "allocation";
        protected override string collectionName => "allocations";

        public AllocationApi(HttpClient httpClient, NomadApiConfig apiConfig) :
            base(httpClient, apiConfig)
        { }

        private Uri buildUriForList(string prefix)
        {
            Uri uri = buildUriForCollection();

            if (!string.IsNullOrEmpty(prefix))
                uri = appendPrefixToUri(uri, prefix);

            return uri;
        }
        private Uri appendPrefixToUri(Uri uri, string prefix)
        {
            BetterUriBuilder builder = new BetterUriBuilder(uri);
            builder.AddQueryField(PREFIX_QUERY_FIELD, prefix);

            return builder.Uri;
        }


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
