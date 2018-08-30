using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Nomad.DotNet.Model;

namespace Nomad.DotNet.API
{
    public class AclPolicies : NomadApi
    {
        protected override string resourceName => "acl/policy";

        protected override string collectionName => "acl/policies";

        public AclPolicies(HttpClient httpClient, NomadApiConfig apiConfig) : base(httpClient, apiConfig)
        { }

        public async Task<AclPolicy> Read(string id)
        {
            Uri uri = buildResourceUri(id);
            AclPolicy policy = await ProcessGetAsync<AclPolicy>(uri);

            return policy;
        }
        public async Task<IList<AclPolicy>> List(string prefix = null)
        {
            Uri uri = buildResourceUriForList(prefix);
            IList<AclPolicy> policies = await ProcessGetAsync<List<AclPolicy>>(uri);

            return policies;
        }
        public async System.Threading.Tasks.Task Create(AclPolicy aclPolicy)
        {
            Uri uri = buildResourceUri(aclPolicy.Name);
            await ProcessPostAsync<string>(uri, aclPolicy);
        }
        public async System.Threading.Tasks.Task Update(AclPolicy aclPolicy)
        {
            await Create(aclPolicy);
        }
        public async System.Threading.Tasks.Task Delete(string policyName)
        {
            Uri uri = buildResourceUri(policyName);
            await ProcessDeleteAsync<string>(uri);
        }
    }
}
