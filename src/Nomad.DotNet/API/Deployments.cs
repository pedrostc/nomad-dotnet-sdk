using Nomad.DotNet.API.DeploymentRequests;
using Nomad.DotNet.Model;
using Nomad.DotNet.UriUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Nomad.DotNet.API
{
    public class Deployments : NomadApi
    {
        public Deployments(HttpClient httpClient, NomadApiConfig apiConfig) : 
            base(httpClient, apiConfig)
        {
        }

        protected override string resourceName => "deployment";

        protected override string collectionName => "deployments";

        public async Task<IList<Deployment>> List(string prefix)
        {
            Uri uri = buildResourceUriForList(prefix);
            IList<Deployment> deployments = await ProcessGetAsync<List<Deployment>>(uri);

            return deployments;
        }

        public async Task<Deployment> Read(string id)
        {
            Uri uri = buildResourceUri(id);
            Deployment deployment = await ProcessGetAsync<Deployment>(uri);

            return deployment;
        }

        private Uri buildUriForDeployment(string id, string path)
        {
            Uri baseUri = buildResourceUri();
            BetterUriBuilder builder = new BetterUriBuilder(baseUri);

            builder.AddPathPart(path);
            builder.AddPathPart(id);

            return builder.Uri;
        }
        public async Task<IList<Allocation>> Allocations(string id)
        {
            Uri uri = buildUriForDeployment(id, "allocations");
            IList<Allocation> allocations = await ProcessGetAsync<IList<Allocation>>(uri);

            return allocations;
        }
        public async Task<DeploymentEvalResponse> Fail(string id)
        {
            Uri uri = buildUriForDeployment(id, "fail");
            DeploymentEvalResponse response = 
                await ProcessPostAsync<DeploymentEvalResponse>(uri);

            return response;
        }
        public async Task<DeploymentEvalResponse> Pause(
            string id, 
            DeploymentPauseRequest requestObj)
        {
            Uri uri = buildUriForDeployment(id, "pause");
            DeploymentEvalResponse response = 
                await ProcessPostAsync<DeploymentEvalResponse>(uri, requestObj);

            return response;
        }
        public async Task<DeploymentEvalResponse> Promote(
            string id, 
            DeploymentPromoteRequest requestObj)
        {
            Uri uri = buildUriForDeployment(id, "promote");
            DeploymentEvalResponse response = 
                await ProcessPostAsync<DeploymentEvalResponse>(uri, requestObj);

            return response;
        }
        public async Task<DeploymentEvalResponse> SetAllocationHealth(
            string id, 
            DeploymentAllocationHealthRequest requestObj)
        {
            Uri uri = buildUriForDeployment(id, "allocation-health");
            DeploymentEvalResponse response = 
                await ProcessPostAsync<DeploymentEvalResponse>(uri, requestObj);

            return response;
        }
    }
}
