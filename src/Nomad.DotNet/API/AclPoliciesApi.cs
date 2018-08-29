using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Nomad.DotNet.Model;

namespace Nomad.DotNet.API
{
    public class AclPoliciesApi : NomadApi<AclPolicy>
    {
        protected override string resourceName => "acl/policy";

        protected override string collectionName => "acl/policies";

        public AclPoliciesApi(HttpClient httpClient, NomadApiConfig apiConfig) : base(httpClient, apiConfig)
        { }

        public async Task<AclPolicy> Read(string id)
        {
            Uri uri = buildUriForResourceId(id);
            AclPolicy policy = await ProcessGetAsync<AclPolicy>(uri);

            return policy;
        }
        public async Task<IList<AclPolicy>> List(string prefix = null)
        {
            Uri uri = buildUriForList(prefix);
            IList<AclPolicy> jobs = await ProcessGetAsync<List<AclPolicy>>(uri);

            return jobs;
        }
        public async System.Threading.Tasks.Task Create(AclPolicy aclPolicy)
        {
            Uri uri = buildUriForResourceId(aclPolicy.Name);
            await ProcessPostAsync<string>(uri, aclPolicy);
        }
        public async System.Threading.Tasks.Task Update(AclPolicy aclPolicy)
        {
            await Create(aclPolicy);
        }
        public async System.Threading.Tasks.Task Delete(string policyName)
        {
            Uri uri = buildUriForResourceId(policyName);
            await ProcessDeleteAsync<string>(uri);
        }
    }
}
