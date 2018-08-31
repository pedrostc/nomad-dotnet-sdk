using Nomad.DotNet.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class SentinelPolicies : NomadApi
    {
        public SentinelPolicies(HttpClient httpClient, NomadApiConfig apiConfig) :
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "sentinel/policy";

        protected override string collectionName => "sentinel/policies";

        public async Task<SentinelPolicy> Read(string policyName)
        {
            Uri uri = buildResourceUri(policyName);
            SentinelPolicy policy = await ProcessGetAsync<SentinelPolicy>(uri);

            return policy;

        }
        public async Task<IList<SentinelPolicy>> List()
        {
            Uri uri = buildCollectionUriForList();
            IList<SentinelPolicy> namespaces = await ProcessGetAsync<List<SentinelPolicy>>(uri);

            return namespaces;
        }

        public async System.Threading.Tasks.Task Create(SentinelPolicy requestObj)
        {
            Uri uri = buildResourceUri();
            await SendPostAsync(uri, requestObj);
        }

        public async System.Threading.Tasks.Task Update(string policyName, SentinelPolicy requestObj)
        {
            Uri uri = buildResourceUri(policyName);
            await SendPostAsync(uri, requestObj);
        }

        public async System.Threading.Tasks.Task Delete(string policyName)
        {
            Uri uri = buildResourceUri(policyName);
            await SendDeleteAsync(uri);
        }
    }
}
