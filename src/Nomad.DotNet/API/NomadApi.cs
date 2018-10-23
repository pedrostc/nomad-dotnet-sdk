using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Nomad.DotNet.Exceptions;
using Nomad.DotNet.UriUtilities;

namespace Nomad.DotNet.API
{
    public abstract class NomadApi
    {
        private string apiVersion = "v1";
        private const string PREFIX_QUERY_FIELD = "prefix";
        protected abstract string resourceName { get; }
        protected abstract string collectionName { get; }

        protected HttpClient httpClient;
        private Uri baseUri;
        private NomadApiConfig apiConfig;

        public NomadApi(
            HttpClient httpClient, 
            NomadApiConfig apiConfig)
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
                case (HttpStatusCode.InternalServerError):
                    string internalErrorMsg = await response.Content.ReadAsStringAsync();
                    throw new SystemException(internalErrorMsg);
            }
        }

        protected Uri buildCollectionUriForList(string prefix = null)
        {
            Uri uri = buildCollectionUri();

            if (!string.IsNullOrEmpty(prefix))
                uri = appendPrefixToUri(uri, prefix);

            return uri;
        }
        private Uri appendPrefixToUri(Uri uri, string prefix)
        {
            BetterUriBuilder builder = new BetterUriBuilder(uri);
            builder.AddQueryField(PREFIX_QUERY_FIELD, prefix);

            return builder.Uri;
        }
        
        protected Uri buildUriFor(string path)
        {
            BetterUriBuilder builder = new BetterUriBuilder(apiConfig.HostUri);
            builder.AddPathPart(apiVersion);
            builder.AddPathPart(path);
            return builder.Uri;
        }

        protected Uri buildResourceUri(string id = null, string method = null)
        {
            BetterUriBuilder builder = new BetterUriBuilder(apiConfig.HostUri);

            builder.AddPathPart(apiVersion);
            builder.AddPathPart(resourceName);

            if(!string.IsNullOrWhiteSpace(id))
                builder.AddPathPart(id);

            if (!string.IsNullOrEmpty(method))
                builder.AddPathPart(method);

            return builder.Uri;
        }
        protected Uri buildCollectionUri(string method = null)
        {
            BetterUriBuilder builder = new BetterUriBuilder(apiConfig.HostUri);

            builder.AddPathPart(apiVersion);
            builder.AddPathPart(collectionName);
            if(!string.IsNullOrEmpty(method))
                builder.AddPathPart(method);

            return builder.Uri;
        }

        protected async Task SendGetAsync(Uri targetUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(targetUri);

            await HandleReponseError(response);
        }
        protected async Task<TResponse> ProcessGetAsync<TResponse>(Uri targetUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(targetUri);

            await HandleReponseError(response);

            TResponse versions = await response.Content.ReadAsAsync<TResponse>();

            return versions;
        }
        protected async Task<string> ProcessGetTextAsync(Uri targetUri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(targetUri);

            await HandleReponseError(response);

            string content = await response.Content?.ReadAsStringAsync();

            return content;
        }
        protected async Task SendPostAsync(Uri targetUri, object requestContent = null)
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(targetUri, requestContent);

            await HandleReponseError(responseMessage);
        }
        protected async Task<TResponse> ProcessPostAsync<TResponse>(Uri targetUri, object requestContent = null)
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(targetUri, requestContent);

            await HandleReponseError(responseMessage);

            TResponse response = await responseMessage.Content?.ReadAsAsync<TResponse>();

            return response;
        }
        protected async Task SendDeleteAsync(Uri targetUri)
        {
            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(targetUri);

            await HandleReponseError(responseMessage);
        }
        protected async Task<TResponse> ProcessDeleteAsync<TResponse>(Uri targetUri)
        {
            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(targetUri);

            await HandleReponseError(responseMessage);

            TResponse response = await responseMessage.Content?.ReadAsAsync<TResponse>();

            return response;
        }
        protected async Task SendPutAsync(Uri targetUri)
        {
            HttpResponseMessage responseMessage = await httpClient.PutAsync(targetUri, null);
            await HandleReponseError(responseMessage);
        }
    }
}
