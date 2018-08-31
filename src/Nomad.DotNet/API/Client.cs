using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Nomad.DotNet.Model;
using Nomad.DotNet.UriUtilities;

//TODO: Implement STREAM methods.
namespace Nomad.DotNet.API
{
    public class Client : NomadApi
    {
        private const string PATH_QUERY_FIELD = "path";
        private const string OFFSET_QUERY_FIELD = "offset";
        private const string LIMIT_QUERY_FIELD = "limit";
        
        private const string FILE_SYSTEM_PATH = "fs";
        private const string ALLOCATION_PATH = "allocation";

        public Client(HttpClient httpClient, NomadApiConfig apiConfig) :
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "client";

        protected override string collectionName => throw new NotImplementedException();

        public async Task<HostStats> Stats()
        {
            Uri uri = buildResourceUri(method: "stats");
            HostStats stats = await ProcessGetAsync<HostStats>(uri);

            return stats;
        }


        private Uri buildAllocationUri(string method, string allocationId)
        {
            Uri baseUri = buildResourceUri();
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);
            builder.AddPathPart(ALLOCATION_PATH);
            builder.AddPathPart(allocationId);
            builder.AddPathPart(method);

            return builder.Uri;
        }

        public async Task<HostStats> AllocationStats(string allocationId)
        {
            
            Uri uri = buildAllocationUri("stats", allocationId);
            HostStats stats = await ProcessGetAsync<HostStats>(uri);

            return stats;
        }

        private Uri buildFileSystemUri(
            string method,
            string allocationId,
            string path)
        {
            Uri baseUri = buildResourceUri();
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);

            builder.AddPathPart(FILE_SYSTEM_PATH);
            builder.AddPathPart(method);
            builder.AddPathPart(allocationId);

            builder.AddQueryField(PATH_QUERY_FIELD, path);

            return builder.Uri;
        }
        public async Task<string> ReadFile(string allocationId, string path)
        {
            Uri uri = buildFileSystemUri("cat", allocationId, path);
            string fileContent = await ProcessGetTextAsync(uri);

            return fileContent;
        }

        private Uri buildReadFileAtOffsetUri(
            string allocationId,
            string path,
            int offset,
            int limit)
        {
            Uri baseUri = buildResourceUri();
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);

            builder.AddPathPart(FILE_SYSTEM_PATH);
            builder.AddPathPart("readat");
            builder.AddPathPart(allocationId);

            builder.AddQueryField(PATH_QUERY_FIELD, path);
            builder.AddQueryField(OFFSET_QUERY_FIELD, offset.ToString());
            builder.AddQueryField(LIMIT_QUERY_FIELD, limit.ToString());

            return builder.Uri;
        }
        public async Task<string> ReadFileAtOffset(
            string allocationId,
            string path,
            int offset,
            int limit)
        {
            
            Uri uri = buildReadFileAtOffsetUri(allocationId, path, offset, limit);
            string fileContent = await ProcessGetTextAsync(uri);

            return fileContent;
        }

        public async Task<IList<AllocFileInfo>> ListFiles(
            string allocationId,
            string path)
        {

            Uri uri = buildFileSystemUri("ls", allocationId, path);
            IList<AllocFileInfo> files = await ProcessGetAsync<List<AllocFileInfo>>(uri);

            return files;
        }
        public async Task<IList<AllocFileInfo>> StatFile(
            string allocationId,
            string path)
        {

            Uri uri = buildFileSystemUri("stat", allocationId, path);
            IList<AllocFileInfo> files = await ProcessGetAsync<List<AllocFileInfo>>(uri);

            return files;
        }

        public async System.Threading.Tasks.Task GCAllocation(string allocationId)
        {
            Uri uri = buildAllocationUri("gc", allocationId);
            await SendGetAsync(uri);
        }
        public async System.Threading.Tasks.Task GCAllAllocations(string allocationId)
        {
            Uri uri = buildResourceUri(method: "gc");
            await SendGetAsync(uri);
        }
    }
}
