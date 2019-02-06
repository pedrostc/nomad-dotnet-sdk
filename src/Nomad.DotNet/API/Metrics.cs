using System;
using System.Net.Http;
using System.Threading.Tasks;

using Nomad.DotNet.UriUtilities;

namespace Nomad.DotNet.API
{
    public class Metrics : NomadApi
    {
        public const string FORMAT_QUERY_FIELD = "format";

        public Metrics(HttpClient httpClient, NomadApiConfig apiConfig)
            : base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "metrics";

        protected override string collectionName => throw new NotImplementedException();

        private Uri buildMetricsUri(MetricsFormat format)
        {
            Uri uri = buildResourceUri();
            BetterUriBuilder builder = new BetterUriBuilder(uri);

            builder.AddQueryField(FORMAT_QUERY_FIELD, format.ToString());

            return builder.Uri;
        }
        public async Task<string> GetMetrics(MetricsFormat format = MetricsFormat.JSON)
        {
            Uri uri = buildMetricsUri(format);
            string metrics = await ProcessGetTextAsync(uri);

            return metrics;
        }
    }

    public enum MetricsFormat
    {
        JSON,
        prometheus
    }
}
