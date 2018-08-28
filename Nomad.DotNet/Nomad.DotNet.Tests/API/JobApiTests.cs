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
        NomadApiConfig apiConfig = new NomadApiConfig
        {
            HostUri = "http://127.0.0.1:4646"
        };

        [TestMethod]
        public void Read_ValidJobId_CallsCorrectUri()
        {
            string jobId = "job-1";
            string jobJson = "{}";
            Uri jobUri = new Uri("http://127.0.0.1:4646/v1/job/job-1");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            var expectedRequest = mockHttp.When(jobUri.AbsoluteUri)
                    .Respond("application/json", jobJson);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);
            Job job = api.Read(jobId).GetAwaiter().GetResult();

            Assert.AreEqual(1, mockHttp.GetMatchCount(expectedRequest));
        }
        [TestMethod]
        public void Read_ExistingJobId_ReturnsPopulatedJob()
        {
            string jobId = "job-1";
            string job1Json = "{\"AllAtOnce\": false, \"Constraints\": null, \"CreateIndex\": 1, \"Datacenters\": [ \"dc1\" ], \"Dispatched\": false, \"ID\": \"job-1\", \"JobModifyIndex\": 12, \"Meta\": null, \"ModifyIndex\": 14, \"Name\": \"job-1\", \"Namespace\": \"default\", \"ParameterizedJob\": null, \"ParentID\": \"\", \"Payload\": null, \"Periodic\": null, \"Priority\": 50, \"Region\": \"global\", \"Stable\": false, \"Status\": \"running\", \"StatusDescription\": \"\", \"Stop\": false, \"SubmitTime\": 1534965921447786200, \"TaskGroups\": [], \"Type\": \"batch\", \"Update\": { \"AutoRevert\": false, \"Canary\": 0, \"HealthCheck\": \"\", \"HealthyDeadline\": 0, \"MaxParallel\": 0, \"MinHealthyTime\": 0, \"ProgressDeadline\": 0, \"Stagger\": 0 }, \"VaultToken\": \"\", \"Version\": 0}";
            Uri job1Uri = new Uri("http://127.0.0.1:4646/v1/job/job-1");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            mockHttp.When(job1Uri.AbsoluteUri)
                    .Respond("application/json", job1Json);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);


            Job job = api.Read(jobId).GetAwaiter().GetResult();

            Assert.IsNotNull(job);
            Assert.AreEqual(jobId, job.Id);
            Assert.AreEqual(1, job.Datacenters.Count);
        }
        [TestMethod]
        [ExpectedException(typeof(EntityNotFound))]
        public void Read_InexistentJobId_ThrowsEntityNotFoundException()
        {
            string jobId = "job-2";
            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);

            Job job = api.Read(jobId).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void List_NoParams_CallsCorrectUri()
        {
            string jobsJson = "[]";
            Uri jobsUri = new Uri("http://127.0.0.1:4646/v1/jobs");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            var expectedRequest = mockHttp.When(jobsUri.AbsoluteUri)
                    .Respond("application/json", jobsJson);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);
            IList<Job> jobs = api.List().GetAwaiter().GetResult();

            Assert.AreEqual(1, mockHttp.GetMatchCount(expectedRequest));
        }
        [TestMethod]
        public void List_WithPrefix_CallsCorrectUriWithQueryString()
        {
            string jobsJson = "[]";
            Uri jobsUri = new Uri("http://127.0.0.1:4646/v1/jobs?prefix=job");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            var expectedRequest = mockHttp.When(jobsUri.AbsoluteUri)
                    .Respond("application/json", jobsJson);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);
            IList<Job> jobs = api.List("job").GetAwaiter().GetResult();

            Assert.AreEqual(1, mockHttp.GetMatchCount(expectedRequest));
        }
        [TestMethod]
        public void List_NoParams_ReturnsPopulatedJobList()
        {
            string jobsJson = "[{\"AllAtOnce\": false, \"Constraints\": null, \"CreateIndex\": 1, \"Datacenters\": [ \"dc1\" ], \"Dispatched\": false, \"ID\": \"job-1\", \"JobModifyIndex\": 12, \"Meta\": null, \"ModifyIndex\": 14, \"Name\": \"job-1\", \"Namespace\": \"default\", \"ParameterizedJob\": null, \"ParentID\": \"\", \"Payload\": null, \"Periodic\": null, \"Priority\": 50, \"Region\": \"global\", \"Stable\": false, \"Status\": \"running\", \"StatusDescription\": \"\", \"Stop\": false, \"SubmitTime\": 1534965921447786200, \"TaskGroups\": [], \"Type\": \"batch\", \"Update\": { \"AutoRevert\": false, \"Canary\": 0, \"HealthCheck\": \"\", \"HealthyDeadline\": 0, \"MaxParallel\": 0, \"MinHealthyTime\": 0, \"ProgressDeadline\": 0, \"Stagger\": 0 }, \"VaultToken\": \"\", \"Version\": 0}]";
            Uri jobsUri = new Uri("http://127.0.0.1:4646/v1/jobs");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            mockHttp.When(jobsUri.AbsoluteUri)
                    .Respond("application/json", jobsJson);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);


            IList<Job> jobs = api.List().GetAwaiter().GetResult();

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
                                        new [] { "8.8.8.8", "-n", "9" }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            CreateRequest request = new CreateRequest(fakeJob);
            HttpClient client = new HttpClient();
            JobApi api = new JobApi(client, apiConfig);

            CreateResponse response = api.Create(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.EvalId);
        }
        [TestMethod]
        [ExpectedException(typeof(BadRequest))]
        public void Create_EmptyObject_ThrowsException()
        {
            Job fakeJob = new Job();

            CreateRequest request = new CreateRequest(fakeJob);
            HttpClient client = new HttpClient();
            JobApi api = new JobApi(client, apiConfig);

            CreateResponse response = api.Create(request).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void Parse_ValidRequest_ReturnsValidJob()
        {
            ParseRequest request = new ParseRequest
            {
                JobHCL = "job \"example\" { type = \"service\" group \"cache\" {} }",
                Canonicalize = true
            };
            HttpClient client = new HttpClient();
            JobApi api = new JobApi(client, apiConfig);

            Job response = api.Parse(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Id);
            Assert.AreEqual("example", response.Id);
            Assert.AreEqual("service", response.Type);
        }
    }
}
