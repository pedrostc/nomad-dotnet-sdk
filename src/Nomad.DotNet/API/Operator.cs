using Nomad.DotNet.Model;
using Nomad.DotNet.UriUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class Operator : NomadApi
    {
        private const string STALE_QUERY_FIELD = "stale";
        private const string ID_QUERY_FIELD = "id";
        private const string ADDRESS_QUERY_FIELD = "address";

        private const string RAFT_PATH = "raft";
        private const string CONFIGURATION_PATH = "configuration";
        private const string PEER_PATH = "peer";

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

            builder.AddPathPart(RAFT_PATH);
            builder.AddPathPart(CONFIGURATION_PATH);

            builder.AddQueryField(STALE_QUERY_FIELD, stale.ToString());

            return builder.Uri;
        }
        public async Task<RaftConfiguration> GetRaftConfiguration(bool stale = false)
        {
            Uri uri = buildRaftConfigurationUri(stale);
            RaftConfiguration raftConfiguration = await ProcessGetAsync<RaftConfiguration>(uri);

            return raftConfiguration;
        }

        private Uri buildRaftPeerUri()
        {
            Uri baseUri = buildResourceUri();
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);

            builder.AddPathPart(RAFT_PATH);
            builder.AddPathPart(PEER_PATH);

            return builder.Uri;
        }
        private Uri buildRemoveRaftPeerUri(string id)
        {
            Uri baseUri = buildRaftPeerUri();
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);

            builder.AddQueryField(ID_QUERY_FIELD, id);

            return builder.Uri;
        }
        private Uri buildRemoveRaftPeerUri(string ip, int port)
        {
            Uri baseUri = buildRaftPeerUri();
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);

            builder.AddQueryField(ADDRESS_QUERY_FIELD, $"{ip}:{port}");

            return builder.Uri;
        }
        // TODO: Find out what is the return type for this api call
        public async Task<object> RemoveRaftPeer(string id)
        {
            Uri uri = buildRemoveRaftPeerUri(id);
            object raftConfiguration = await ProcessDeleteAsync<object>(uri);

            return raftConfiguration;
        }
        public async Task<object> RemoveRaftPeer(string ip, int port)
        {
            Uri uri = buildRemoveRaftPeerUri(ip, port);
            object raftConfiguration = await ProcessDeleteAsync<object>(uri);

            return raftConfiguration;
        }
    }
}
