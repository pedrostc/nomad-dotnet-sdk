using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Nomad.DotNet.Model;

namespace Nomad.DotNet.API
{
    public class AclTokens : NomadApi
    {
        public AclTokens(HttpClient httpClient, NomadApiConfig apiConfig) : base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "acl/token";

        protected override string collectionName => "acl/tokens";

        private string bootstrapPath => "acl/bootstrap";

        public async Task<AclToken> Bootstrap()
        {
            Uri uri = buildUriFor(bootstrapPath);
            AclToken token = await ProcessPostAsync<AclToken>(uri, null);

            return token;
        }

        public async Task<AclToken> Read(string id)
        {
            Uri uri = buildResourceUri(id);
            AclToken token = await ProcessGetAsync<AclToken>(uri);

            return token;
        }

        public async Task<AclToken> ReadSelf()
        {
            string methodName = "self";
            Uri uri = buildResourceUri(method: methodName);
            AclToken token = await ProcessGetAsync<AclToken>(uri);

            return token;
        }

        public async Task<IList<AclToken>> List()
        {
            Uri uri = buildResourceUriForList();
            IList<AclToken> tokens = await ProcessGetAsync<List<AclToken>>(uri);

            return tokens;
        }

        public async Task<AclToken> Create(AclToken requestObj)
        {
            Uri uri = buildResourceUri();
            AclToken token = await ProcessPostAsync<AclToken>(uri, requestObj);

            return token;
        }

        public async Task<AclToken> Update(string acessorId, AclToken requestObj)
        {
            Uri uri = buildResourceUri(acessorId);
            AclToken token = await ProcessPostAsync<AclToken>(uri, requestObj);

            return token;
        }

        public async System.Threading.Tasks.Task Delete(string acessorId)
        {
            Uri uri = buildResourceUri(acessorId);
            await SendDeleteAsync(uri);
        }
    }
}
