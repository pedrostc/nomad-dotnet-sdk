using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Nomad.DotNet.API.AgentRequests;
using Nomad.DotNet.Model;
using Nomad.DotNet.UriUtilities;

namespace Nomad.DotNet.API
{
    public class Agents : NomadApi<Agent>
    {
        public Agents(HttpClient httpClient, NomadApiConfig apiConfig) : base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "agent";

        protected override string collectionName => "agent";

        public async Task<IList<AgentMember>> Members()
        {
            Uri uri = buildResourceUri(method: "members");
            List<AgentMember> members = await ProcessGetAsync<List<AgentMember>>(uri);

            return members;
        }

        public async Task<IList<string>> Servers()
        {
            Uri uri = buildResourceUri(method: "servers");
            List<string> servers = await ProcessGetAsync<List<string>>(uri);

            return servers;
        }

        private Uri buildUriForServers(Uri baseUri, List<string> servers)
        {
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);

            servers.ForEach(server => builder.AddQueryField("address", server));

            return builder.Uri;
        }
        public async System.Threading.Tasks.Task UpdateServers(List<string> servers)
        {
            Uri baseUri = buildResourceUri(method: "servers");
            Uri uri = buildUriForServers(baseUri, servers);
            await ProcessPostAsync<string>(uri);
        }

        public async Task<AgentSelf> QuerySelf()
        {
            Uri uri = buildResourceUri("self");
            AgentSelf agent = await ProcessGetAsync<AgentSelf>(uri);

            return agent;
        }

        public async Task<AgentJoinResponse> Join(List<string> servers)
        {
            Uri baseUri = buildResourceUri(method: "join");
            Uri uri = buildUriForServers(baseUri, servers);
            AgentJoinResponse response = await ProcessPostAsync<AgentJoinResponse>(uri);

            return response;
        }

        private Uri buildUriForForceLeave(Uri baseUri, string nodeName)
        {
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);

           builder.AddQueryField("node", nodeName);

            return builder.Uri;
        }
        public async System.Threading.Tasks.Task ForceLeave(string nodeName)
        {
            Uri baseUri = buildResourceUri(method: "force-leave");
            Uri uri = buildUriForForceLeave(baseUri, nodeName);
            await ProcessPostAsync<string>(uri);
        }

        public async Task<AgentHealthResponse> Health()
        {
            Uri uri = buildResourceUri(method: "health");
            AgentHealthResponse agentHealth = 
                await ProcessGetAsync<AgentHealthResponse>(uri);

            return agentHealth;
        }
    }
}
