using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Nomad.DotNet.Exceptions;
using Nomad.DotNet.UriUtilities;



namespace Nomad.DotNet.API
{
    public abstract class NomadApi<T> where T : Model.ApiObject<T>
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

        protected async Task HandleReponseError(HttpResponseMessage response)
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

        protected Uri buildUriForList(string prefix)
        {
            Uri uri = buildUriForCollection();

            if (!string.IsNullOrEmpty(prefix))
                uri = appendPrefixToUri(uri, prefix);

            return uri;
        }
        protected Uri appendPrefixToUri(Uri uri, string prefix)
        {
            BetterUriBuilder builder = new BetterUriBuilder(uri);
            builder.AddQueryField(PREFIX_QUERY_FIELD, prefix);

            return builder.Uri;
        }
        protected Uri buildUriForResourceId(string id, string method = null)
        {
            BetterUriBuilder builder = new BetterUriBuilder(apiConfig.HostUri);

            builder.AddPathPart(apiVersion);
            builder.AddPathPart(resourceName);
            builder.AddPathPart(id);

            if (!string.IsNullOrEmpty(method))
                builder.AddPathPart(method);

            return builder.Uri;
        }
        protected Uri buildUriForCollection(string method = null)
        {
            BetterUriBuilder builder = new BetterUriBuilder(apiConfig.HostUri);

            builder.AddPathPart(apiVersion);
            builder.AddPathPart(collectionName);
            if(!string.IsNullOrEmpty(method))
                builder.AddPathPart(method);

            return builder.Uri;
        }

        protected async Task<TResponse> ProcessGetAsync<TResponse>(Uri targetUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(targetUri);

            await HandleReponseError(response);

            TResponse versions = await response.Content.ReadAsAsync<TResponse>();

            return versions;
        }
        protected async Task<TResponse> ProcessPostAsync<TResponse>(Uri targetUri, object requestContent)
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(targetUri, requestContent);

            await HandleReponseError(responseMessage);

            TResponse response = await responseMessage.Content.ReadAsAsync<TResponse>();

            return response;
        }
        protected async Task<TResponse> ProcessDeleteAsync<TResponse>(Uri targetUri)
        {
            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(targetUri);

            await HandleReponseError(responseMessage);

            TResponse response = await responseMessage.Content.ReadAsAsync<TResponse>();

            return response;
        }
    }
}
