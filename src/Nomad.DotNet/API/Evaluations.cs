using Nomad.DotNet.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class Evaluations : NomadApi
    {
        public Evaluations(HttpClient httpClient, NomadApiConfig apiConfig) : 
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "evaluation";

        protected override string collectionName => "evaluations";

        public async Task<Evaluation> Read(string id)
        {
            Uri uri = buildResourceUri(id);
            Evaluation evaluation = await ProcessGetAsync<Evaluation>(uri);

            return evaluation;

        }
        public async Task<IList<Evaluation>> List(string prefix = null)
        {
            Uri uri = buildResourceUriForList(prefix);
            IList<Evaluation> evaluations = await ProcessGetAsync<List<Evaluation>>(uri);

            return evaluations;
        }
        public async Task<IList<Allocation>> Allocations(string evalId)
        {
            Uri uri = buildResourceUri(evalId, "allocations");
            IList<Allocation> allocations = await ProcessGetAsync<List<Allocation>>(uri);

            return allocations;
        }
    }
}
