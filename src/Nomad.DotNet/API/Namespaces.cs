using Nomad.DotNet.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class Namespaces : NomadApi
    {
        public Namespaces(HttpClient httpClient, NomadApiConfig apiConfig) :
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "namespace";

        protected override string collectionName => "namespaces";

        public async Task<Namespace> Read(string namespaceName)
        {
            Uri uri = buildResourceUri(namespaceName);
            Namespace namespaceResult = await ProcessGetAsync<Namespace>(uri);

            return namespaceResult;

        }
        public async Task<IList<Namespace>> List(string prefix = null)
        {
            Uri uri = buildCollectionUriForList(prefix);
            IList<Namespace> namespaces = await ProcessGetAsync<List<Namespace>>(uri);

            return namespaces;
        }

        public async System.Threading.Tasks.Task Create(Namespace requestObj)
        {
            Uri uri = buildResourceUri();
            await SendPostAsync(uri, requestObj);
        }

        public async System.Threading.Tasks.Task Update(string namespaceName, Namespace requestObj)
        {
            Uri uri = buildResourceUri(namespaceName);
            await SendPostAsync(uri, requestObj);
        }

        public async System.Threading.Tasks.Task Delete(string namespaceName)
        {
            Uri uri = buildResourceUri(namespaceName);
            await SendDeleteAsync(uri);
        }
    }
}
