using Nomad.DotNet.Model;
using Nomad.DotNet.UriUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class Quotas : NomadApi
    {
        public Quotas(HttpClient httpClient, NomadApiConfig apiConfig) :
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "quota";

        protected override string collectionName => "quotas";

        public async Task<QuotaSpec> Read(string quotaId)
        {
            Uri uri = buildResourceUri(quotaId);
            QuotaSpec quota = await ProcessGetAsync<QuotaSpec>(uri);

            return quota;

        }
        public async Task<IList<QuotaSpec>> List(string prefix = null)
        {
            Uri uri = buildCollectionUriForList(prefix);
            IList<QuotaSpec> quotas = await ProcessGetAsync<List<QuotaSpec>>(uri);

            return quotas;
        }
        public async System.Threading.Tasks.Task Create(QuotaSpec quota)
        {
            Uri uri = buildResourceUri();
            await SendPostAsync(uri, quota);
        }

        public async System.Threading.Tasks.Task Update(string quotaId, QuotaSpec quota)
        {
            Uri uri = buildResourceUri(quotaId);
            await SendPostAsync(uri, quota);
        }
        public async System.Threading.Tasks.Task Delete(string quotaId)
        {
            Uri uri = buildResourceUri(quotaId);
            await SendDeleteAsync(uri);
        }
        public async Task<IList<QuotaUsage>> ListQuotaUsages()
        {
            Uri uri = buildUriFor("quota-usages");
            IList<QuotaUsage> usages = await ProcessGetAsync<IList<QuotaUsage>>(uri);

            return usages;
        }
        private Uri buildReadQuotaUsageUri(string quotaId)
        {
            Uri baseUri = buildResourceUri();
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);

            builder.AddPathPart("usage");
            builder.AddPathPart(quotaId);

            return builder.Uri;
        }
        public async Task<QuotaUsage> ReadQuotaUsages(string quotaId)
        {
            Uri uri = buildReadQuotaUsageUri(quotaId);
            QuotaUsage usage = await ProcessGetAsync<QuotaUsage>(uri);

            return usage;
        }
    }
}
