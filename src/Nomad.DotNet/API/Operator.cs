using Nomad.DotNet.UriUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Nomad.DotNet.API
{
    public class Operator : NomadApi
    {
        private const string STALE_QUERY_FIELD = "stale";

        public Operator(HttpClient httpClient, NomadApiConfig apiConfig)
            : base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "operator";

        protected override string collectionName => throw new NotImplementedException();

        private Uri buildRaftConfigurationUri(bool stale)
        {
            Uri baseUri = buildResourceUri();
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);

            builder.AddQueryField(STALE_QUERY_FIELD, stale.ToString());

            return builder.Uri;
        }
        public object GetRaftConfiguration(bool stale = false)
        {
            Uri uri = buildRaftConfigurationUri(stale);


            return null;
        }
    }
}
