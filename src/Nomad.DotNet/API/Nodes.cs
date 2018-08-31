using Nomad.DotNet.API.NodeRequests;
using Nomad.DotNet.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class Nodes : NomadApi
    {
        public Nodes(HttpClient httpClient, NomadApiConfig apiConfig) : 
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "node";

        protected override string collectionName => "nodes";
        
        public async Task<Node> Read(string nodeId)
        {
            Uri uri = buildResourceUri(nodeId);
            Node node = await ProcessGetAsync<Node>(uri);

            return node;

        }
        public async Task<IList<Node>> List(string prefix = null)
        {
            Uri uri = buildCollectionUriForList(prefix);
            IList<Node> nodes = await ProcessGetAsync<List<Node>>(uri);

            return nodes;
        }
        public async Task<IList<Allocation>> Allocations(string nodeId)
        {
            Uri uri = buildResourceUri(nodeId, "allocations");
            IList<Allocation> allocations = 
                await ProcessGetAsync<IList<Allocation>>(uri);

            return allocations;
        }
        public async Task<dynamic> Evaluate(string nodeId)
        {
            Uri uri = buildResourceUri(nodeId, "evaluate");
            dynamic evaluation =
                await ProcessPostAsync<dynamic>(uri);

            return evaluation;
        }
        public async Task<dynamic> ToggleDrain(
            string nodeId, 
            NodeToggleDrainRequest requestObj)
        {
            Uri uri = buildResourceUri(nodeId, "drain");
            dynamic evaluation =
                await ProcessPostAsync<dynamic>(uri, requestObj);

            return evaluation;
        }
        public async Task<dynamic> Purge(string nodeId)
        {
            Uri uri = buildResourceUri(nodeId, "purge");
            dynamic evaluation =
                await ProcessPostAsync<dynamic>(uri);

            return evaluation;
        }
    }
}
