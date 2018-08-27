using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nomad.DotNet.Model;
using Nomad.DotNet.API;
using RichardSzalay.MockHttp;
using System;
using Nomad.DotNet.Exceptions;
using System.Collections.Generic;
using Nomad.DotNet.API.JobRequests;
using System.Net.Http;

namespace Nomad.DotNet.Tests.API
{
    [TestClass]
    public class JobApiTests
    {
        UriBuilder baseUriBuilder = new UriBuilder("http", "127.0.0.1", 4646);

        [TestMethod]
        public void GetByIdAsync_ValidJobId_CallsCorrectUri()
        {
            string jobId = "job-1";
            string jobJson = "{}";
            Uri jobUri = new Uri("http://127.0.0.1:4646/v1/job/job-1");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            var expectedRequest = mockHttp.When(jobUri.AbsoluteUri)
                    .Respond("application/json", jobJson);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), baseUriBuilder.Uri);
            Job job = api.GetByIdAsync(jobId).GetAwaiter().GetResult();

            Assert.AreEqual(1, mockHttp.GetMatchCount(expectedRequest));
        }

        [TestMethod]
        public void GetByIdAsync_ExistingJobId_ReturnsPopulatedJob()
        {
            string jobId = "job-1";
            string job1Json = "{\"AllAtOnce\": false, \"Constraints\": null, \"CreateIndex\": 1, \"Datacenters\": [ \"dc1\" ], \"Dispatched\": false, \"ID\": \"job-1\", \"JobModifyIndex\": 12, \"Meta\": null, \"ModifyIndex\": 14, \"Name\": \"job-1\", \"Namespace\": \"default\", \"ParameterizedJob\": null, \"ParentID\": \"\", \"Payload\": null, \"Periodic\": null, \"Priority\": 50, \"Region\": \"global\", \"Stable\": false, \"Status\": \"running\", \"StatusDescription\": \"\", \"Stop\": false, \"SubmitTime\": 1534965921447786200, \"TaskGroups\": [], \"Type\": \"batch\", \"Update\": { \"AutoRevert\": false, \"Canary\": 0, \"HealthCheck\": \"\", \"HealthyDeadline\": 0, \"MaxParallel\": 0, \"MinHealthyTime\": 0, \"ProgressDeadline\": 0, \"Stagger\": 0 }, \"VaultToken\": \"\", \"Version\": 0}";
            Uri job1Uri = new Uri("http://127.0.0.1:4646/v1/job/job-1");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            mockHttp.When(job1Uri.AbsoluteUri)
                    .Respond("application/json", job1Json);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), baseUriBuilder.Uri);


            Job job = api.GetByIdAsync(jobId).GetAwaiter().GetResult();

            Assert.IsNotNull(job);
            Assert.AreEqual(jobId, job.Id);
            Assert.AreEqual(1, job.Datacenters.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFound))]
        public void GetByIdAsync_InexistentJobId_ThrowsEntityNotFoundException()
        {
            string jobId = "job-2";
            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            JobApi api = new JobApi(mockHttp.ToHttpClient(), baseUriBuilder.Uri);

            Job job = api.GetByIdAsync(jobId).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void ListAsync_NoParams_CallsCorrectUri()
        {
            string jobsJson = "[]";
            Uri jobsUri = new Uri("http://127.0.0.1:4646/v1/jobs");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            var expectedRequest = mockHttp.When(jobsUri.AbsoluteUri)
                    .Respond("application/json", jobsJson);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), baseUriBuilder.Uri);
            IList<Job> jobs = api.ListAsync().GetAwaiter().GetResult();

            Assert.AreEqual(1, mockHttp.GetMatchCount(expectedRequest));
        }

        [TestMethod]
        public void ListAsync_NoParams_ReturnsPopulatedJobList()
        {
            string jobsJson = "[{\"AllAtOnce\": false, \"Constraints\": null, \"CreateIndex\": 1, \"Datacenters\": [ \"dc1\" ], \"Dispatched\": false, \"ID\": \"job-1\", \"JobModifyIndex\": 12, \"Meta\": null, \"ModifyIndex\": 14, \"Name\": \"job-1\", \"Namespace\": \"default\", \"ParameterizedJob\": null, \"ParentID\": \"\", \"Payload\": null, \"Periodic\": null, \"Priority\": 50, \"Region\": \"global\", \"Stable\": false, \"Status\": \"running\", \"StatusDescription\": \"\", \"Stop\": false, \"SubmitTime\": 1534965921447786200, \"TaskGroups\": [], \"Type\": \"batch\", \"Update\": { \"AutoRevert\": false, \"Canary\": 0, \"HealthCheck\": \"\", \"HealthyDeadline\": 0, \"MaxParallel\": 0, \"MinHealthyTime\": 0, \"ProgressDeadline\": 0, \"Stagger\": 0 }, \"VaultToken\": \"\", \"Version\": 0}]";
            Uri jobsUri = new Uri("http://127.0.0.1:4646/v1/jobs");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            mockHttp.When(jobsUri.AbsoluteUri)
                    .Respond("application/json", jobsJson);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), baseUriBuilder.Uri);


            IList<Job> jobs = api.ListAsync().GetAwaiter().GetResult();

            Assert.IsNotNull(jobs);
            Assert.AreEqual(1, jobs.Count);
            Assert.AreEqual("job-1", jobs[0].Id);
        }

        [TestMethod]
        public void CreateAsync_ValidRequest_ReturnValidEvaluation()
        {
            Job fakeJob = new Job
            {
                Id = "job-10",
                Name = "job-10",
                Datacenters = new[] { "dc1" },
                Type = "batch",
                Version = 0,
                TaskGroups = new List<TaskGroup>
                {
                    new TaskGroup
                    {
                        Name="ping",
                        Tasks = new List<Task>
                        {
                            new Task
                            {
                                Name="ping-google",
                                Driver="raw_exec",
                                Config = new Dictionary<string, object>
                                {
                                    { "command", "ping" },
                                    {
                                        "args",
                                        new [] { "8.8.8.8" }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            CreationRequest request = new CreationRequest
            {
                Job = fakeJob
            };

            HttpClient client = new HttpClient();
            JobApi api = new JobApi(client, baseUriBuilder.Uri);

            CreationResponse response = api.CreateAsync(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.EvalId);
        }
    }
}
