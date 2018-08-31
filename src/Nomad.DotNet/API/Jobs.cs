using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Nomad.DotNet.API.JobRequests;
using Nomad.DotNet.Model;
using JobDispatchResponse = Nomad.DotNet.API.JobRequests.JobDispatchResponse;
using JobStabilityResponse = Nomad.DotNet.API.JobRequests.JobStabilityResponse;
using JobPlanResponse = Nomad.DotNet.API.JobRequests.JobPlanResponse;

namespace Nomad.DotNet.API
{
    public class Jobs : NomadApi
    {
        protected override string resourceName => "job";
        protected override string collectionName => "jobs";
        public Jobs(HttpClient httpClient, NomadApiConfig apiConfig) : base(httpClient, apiConfig)
        { }

        public async Task<Job> Read(string id)
        {
            Uri uri = buildResourceUri(id);
            Job job = await ProcessGetAsync<Job>(uri);

            return job;

        }
        public async Task<IList<Job>> List(string prefix = null)
        {
            Uri uri = buildCollectionUriForList(prefix);
            IList<Job> jobs = await ProcessGetAsync<List<Job>>(uri);

            return jobs;
        }

        public async Task<JobCreateResponse> Create(JobCreateRequest requestObj)
        {
            Uri uri = buildCollectionUri();
            JobCreateResponse response = await ProcessPostAsync<JobCreateResponse>(uri, requestObj);

            return response;
        }

        public async Task<Job> Parse(JobParseRequest requestObj)
        {
            Uri uri = buildCollectionUri("parse");
            Job response = await ProcessPostAsync<Job>(uri, requestObj);

            return response;
        }

        public async Task<JobVersionsResponse> Versions(string id)
        {
            string methodName = "versions";
            Uri uri = buildResourceUri(id, methodName);
            JobVersionsResponse versions = await ProcessGetAsync<JobVersionsResponse>(uri);

            return versions;
        }

        public async Task<IList<Allocation>> Allocations(string id)
        {
            string methodName = "allocations";
            Uri uri = buildResourceUri(id, methodName);
            IList<Allocation> allocations = await ProcessGetAsync<List<Allocation>>(uri);

            return allocations;
        }

        public async Task<IList<Evaluation>> Evaluations(string id)
        {
            string methodName = "evaluations";
            Uri uri = buildResourceUri(id, methodName);
            IList<Evaluation> evaluations = await ProcessGetAsync<List<Evaluation>>(uri);

            return evaluations;
        }

        public async Task<IList<Deployment>> Deployments(string id)
        {
            string methodName = "deployments";
            Uri uri = buildResourceUri(id, methodName);
            IList<Deployment> deployments = await ProcessGetAsync<List<Deployment>>(uri);

            return deployments;
        }

        public async Task<Deployment> LatestDeployment(string id)
        {
            string methodName = "deployment";
            Uri uri = buildResourceUri(id, methodName);
            Deployment latestDeployment = await ProcessGetAsync<Deployment>(uri);

            return latestDeployment;
        }

        public async Task<JobSummary> Summary(string id)
        {
            string methodName = "summary";
            Uri uri = buildResourceUri(id, methodName);
            JobSummary jobSummary = await ProcessGetAsync<JobSummary>(uri);

            return jobSummary;
        }

        public async Task<JobUpdateResponse> Update(string id, JobUpdateRequest requestObj)
        {
            Uri uri = buildResourceUri(id);
            JobUpdateResponse updateResponse = await ProcessPostAsync<JobUpdateResponse>(uri, requestObj);

            return updateResponse;
        }

        public async Task<JobDispatchResponse> Dispatch(string id, JobDispatchRequest requestObj)
        {
            string methodName = "dispatch";
            Uri uri = buildResourceUri(id, methodName);
            JobDispatchResponse dispatchResponse = 
                await ProcessPostAsync<JobDispatchResponse>(uri, requestObj);

            return dispatchResponse;
        }

        public async Task<JobRevertResponse> Revert(string id, JobRevertRequest requestObj)
        {
            string methodName = "revert";
            Uri uri = buildResourceUri(id, methodName);
            JobRevertResponse revertResponse = 
                await ProcessPostAsync<JobRevertResponse>(uri, requestObj);

            return revertResponse;
        }

        public async Task<JobStabilityResponse> Stability(string id, JobStabilityRequest requestObj)
        {
            string methodName = "stability";
            Uri uri = buildResourceUri(id, methodName);
            JobStabilityResponse stabilityResponse = 
                await ProcessPostAsync<JobStabilityResponse>(uri, requestObj);

            return stabilityResponse;
        }

        public async Task<JobEvaluateResponse> Evaluate(string id, JobEvaluateRequest requestObj)
        {
            string methodName = "evaluate";
            Uri uri = buildResourceUri(id, methodName);
            JobEvaluateResponse evaluateResponse = 
                await ProcessPostAsync<JobEvaluateResponse>(uri, requestObj);

            return evaluateResponse;
        }

        public async Task<JobPlanResponse> Plan(string id, JobPlanRequest requestObj)
        {
            string methodName = "plan";
            Uri uri = buildResourceUri(id, methodName);
            JobPlanResponse planResponse =
                await ProcessPostAsync<JobPlanResponse>(uri, requestObj);

            return planResponse;
        }

        public async Task<JobPeriodicForceResponse> PeriodicForce(string id)
        {
            string methodName = "periodic/force";
            Uri uri = buildResourceUri(id, methodName);
            JobPeriodicForceResponse periodicForceResponse =
                await ProcessPostAsync<JobPeriodicForceResponse>(uri, null);

            return periodicForceResponse;
        }

        public async Task<JobStopResponse> Stop(string id)
        {
            Uri uri = buildResourceUri(id);
            JobStopResponse stopResponse =
                await ProcessDeleteAsync<JobStopResponse>(uri);

            return stopResponse;
        }
    }
}
