using Nomad.DotNet.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public abstract class NomadApi<T> where T : ApiObject<T>
    {
        private string apiVersion = "v1";
        protected abstract string resourceName { get; }

        HttpClient httpClient;
        UriBuilder builder;
        
        public NomadApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            builder = new UriBuilder("http", "127.0.0.1", 4646);
        }

        protected Uri buildUriForResourceId(string id)
        {
            Uri relativeUri = new Uri($"{apiVersion}/{resourceName}/{id}", UriKind.Relative);
            Uri fullUri = new Uri(builder.Uri, relativeUri);

            return fullUri;
        }

        public async Task<T> GetAsync(string id)
        {
            Uri uri = buildUriForResourceId(id);

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            T resource = await response.Content.ReadAsAsync<T>();

            return resource;
        }
    }
}
