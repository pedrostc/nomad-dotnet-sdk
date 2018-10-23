using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
        private const string TASK_QUERY_FIELD = "task";
        private const string FOLLOW_QUERY_FIELD = "follow";
        private const string TYPE_QUERY_FIELD = "type";
        private const string ORIGIN_QUERY_FIELD = "origin";
        private const string PLAIN_QUERY_FIELD = "plain";
        
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

        private Uri buildStreamLogsUri(
            string allocationId,
            string task,
            LogType type,
            OffsetOrigin origin,
            int offset,
            bool follow,
            bool plain)
        {
            Uri baseUri = buildResourceUri();
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);

            builder.AddPathPart(FILE_SYSTEM_PATH);
            builder.AddPathPart("logs");
            builder.AddPathPart(allocationId);

            builder.AddQueryField(TASK_QUERY_FIELD, task);
            builder.AddQueryField(FOLLOW_QUERY_FIELD, follow.ToString());
            builder.AddQueryField(TYPE_QUERY_FIELD, type.ToString());
            builder.AddQueryField(OFFSET_QUERY_FIELD, offset.ToString());
            builder.AddQueryField(ORIGIN_QUERY_FIELD, origin.ToString());
            builder.AddQueryField(PLAIN_QUERY_FIELD, plain.ToString());

            return builder.Uri;
        }
        public async Task<FileContent> StreamLogs(
            string allocationId,
            string task,
            LogType type,
            int offset = 0,
            OffsetOrigin origin = OffsetOrigin.start,
            bool follow = false,
            bool plain = false)
        {

            Uri uri = buildStreamLogsUri(allocationId, 
                task, type, origin, offset, follow, plain);
            string fileContentString = await ProcessGetTextAsync(uri);
            FileContent fileContent = JsonConvert.DeserializeObject<FileContent>(fileContentString);
            
            return fileContent;
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

    public enum OffsetOrigin
    {
        start,
        end
    }

    public enum LogType
    {
        stderr,
        stdout
    }
}
