using Nomad.DotNet.Exceptions;
using Nomad.DotNet.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public abstract class NomadApi<T> where T : ApiObject<T>
    {
        private string apiVersion = "v1";
        protected abstract string resourceName { get; }
        protected abstract string collectionName { get; }

        protected HttpClient httpClient;
        Uri baseUri;
        
        public NomadApi(HttpClient httpClient, Uri baseUri)
        {
            this.httpClient = httpClient;
            this.baseUri = baseUri;
        }

        private void HandleReponseError(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case (HttpStatusCode.NotFound):
                    throw new EntityNotFound();
            }
        }

        protected Uri buildUriForResource()
        {
            Uri relativeUri = new Uri($"{apiVersion}/{resourceName}", UriKind.Relative);
            Uri fullUri = new Uri(baseUri, relativeUri);

            return fullUri;
        }
        protected Uri buildUriForResourceId(string id)
        {
            Uri relativeUri = new Uri($"{apiVersion}/{resourceName}/{id}", UriKind.Relative);
            Uri fullUri = new Uri(baseUri, relativeUri);

            return fullUri;
        }
        protected Uri buildUriForCollection()
        {
            Uri relativeUri = new Uri($"{apiVersion}/{collectionName}", UriKind.Relative);
            Uri fullUri = new Uri(baseUri, relativeUri);

            return fullUri;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            Uri uri = buildUriForResourceId(id);

            HttpResponseMessage response = await httpClient.GetAsync(uri);
           
            if(!response.IsSuccessStatusCode)
            {
                HandleReponseError(response);
            }

            T resource = await response.Content.ReadAsAsync<T>();
            return resource;

        }
        public async Task<IList<T>> ListAsync()
        {
            Uri uri = buildUriForCollection();

            HttpResponseMessage response = await httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                HandleReponseError(response);
            }

            IList<T> resource = await response.Content.ReadAsAsync<List<T>>();
            return resource;
        }
        public async System.Threading.Tasks.Task CreateAsync()
        {

        }
    }
}
