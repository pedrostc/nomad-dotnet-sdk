using System;
using System.Net;
using System.Net.Http;

using Nomad.DotNet.Exceptions;
using Nomad.DotNet.Model;
using Nomad.DotNet.UriUtilities;

namespace Nomad.DotNet.API
{
    public abstract class NomadApi<T> where T : ApiObject<T>
    {
        private string apiVersion = "v1";
        protected abstract string resourceName { get; }
        protected abstract string collectionName { get; }

        protected HttpClient httpClient;
        private Uri baseUri;
        private NomadApiConfig apiConfig;

        public NomadApi(HttpClient httpClient, NomadApiConfig apiConfig)
        {
            this.httpClient = httpClient;
            this.baseUri = new Uri(apiConfig.HostUri);
            this.apiConfig = apiConfig;
        }

        protected async System.Threading.Tasks.Task HandleReponseError(HttpResponseMessage response)
        {
            switch (response.StatusCode)
            {
                case (HttpStatusCode.NotFound):
                    throw new EntityNotFound();
                case (HttpStatusCode.BadRequest):
                    string errorMsg = await response.Content.ReadAsStringAsync();
                    throw new BadRequest(errorMsg);
            }
        }

        protected Uri buildUriForResourceId(string id)
        {
            BetterUriBuilder builder = new BetterUriBuilder(apiConfig.HostUri);

            builder.AddPathPart(apiVersion);
            builder.AddPathPart(resourceName);
            builder.AddPathPart(id);

            return builder.Uri;
        }
        protected Uri buildUriForCollection()
        {
            BetterUriBuilder builder = new BetterUriBuilder(apiConfig.HostUri);

            builder.AddPathPart(apiVersion);
            builder.AddPathPart(collectionName);

            return builder.Uri;
        }
    }
}
