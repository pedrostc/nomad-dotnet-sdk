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
                                        new [] { "8.8.8.8", "-n", "10" }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            JobCreateRequest request = new JobCreateRequest(fakeJob);
            HttpClient client = new HttpClient();
            JobApi api = new JobApi(client, apiConfig);

            JobCreateResponse response = api.Create(request).GetAwaiter().GetResult();

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.EvalId);
        }
        [TestMethod]
        [ExpectedException(typeof(BadRequest))]
        public void Create_EmptyObject_ThrowsException()
        {
            Job fakeJob = new Job();

            JobCreateRequest request = new JobCreateRequest(fakeJob);
            HttpClient client = new HttpClient();
            JobApi api = new JobApi(client, apiConfig);

            JobCreateResponse response = api.Create(request).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void Parse_ValidRequest_ReturnsValidJob()
        {
            JobParseRequest request = new JobParseRequest
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


        [TestMethod]
        public void Versions_ExistingJobId_CallsCorrectUri()
        {
            string jobId = "job-1";
            string versionsJson = "{\"Diffs\":null,\"Index\":3386,\"KnownLeader\":true,\"LastContact\":0,\"Versions\":[{\"AllAtOnce\":false,\"Constraints\":null,\"CreateIndex\":1489,\"Datacenters\":[\"dc1\"],\"Dispatched\":false,\"ID\":\"job-10\",\"JobModifyIndex\":3386,\"Meta\":null,\"ModifyIndex\":3386,\"Name\":\"job-10\",\"Namespace\":\"default\",\"ParameterizedJob\":null,\"ParentID\":\"\",\"Payload\":null,\"Periodic\":null,\"Priority\":50,\"Region\":\"global\",\"Stable\":false,\"Status\":\"dead\",\"StatusDescription\":\"\",\"Stop\":false,\"SubmitTime\":1535552347596677700,\"TaskGroups\":[{\"Constraints\":null,\"Count\":1,\"EphemeralDisk\":{\"Migrate\":false,\"SizeMB\":300,\"Sticky\":false},\"Meta\":null,\"Migrate\":null,\"Name\":\"ping\",\"ReschedulePolicy\":{\"Attempts\":1,\"Delay\":5000000000,\"DelayFunction\":\"constant\",\"Interval\":86400000000000,\"MaxDelay\":0,\"Unlimited\":false},\"RestartPolicy\":{\"Attempts\":3,\"Delay\":15000000000,\"Interval\":86400000000000,\"Mode\":\"fail\"},\"Tasks\":[{\"Artifacts\":null,\"Config\":{\"command\":\"ping\",\"args\":[\"8.8.8.8\",\"-n\",\"10\"]},\"Constraints\":null,\"DispatchPayload\":null,\"Driver\":\"raw_exec\",\"Env\":null,\"KillSignal\":\"\",\"KillTimeout\":5000000000,\"Leader\":false,\"LogConfig\":{\"MaxFileSizeMB\":10,\"MaxFiles\":10},\"Meta\":null,\"Name\":\"ping-google\",\"Resources\":{\"CPU\":100,\"DiskMB\":0,\"IOPS\":0,\"MemoryMB\":300,\"Networks\":null},\"Services\":null,\"ShutdownDelay\":0,\"Templates\":null,\"User\":\"\",\"Vault\":null}],\"Update\":null}],\"Type\":\"batch\",\"Update\":{\"AutoRevert\":false,\"Canary\":0,\"HealthCheck\":\"\",\"HealthyDeadline\":0,\"MaxParallel\":0,\"MinHealthyTime\":0,\"ProgressDeadline\":0,\"Stagger\":0},\"VaultToken\":\"\",\"Version\":1},{\"AllAtOnce\":false,\"Constraints\":null,\"CreateIndex\":1489,\"Datacenters\":[\"dc1\"],\"Dispatched\":false,\"ID\":\"job-10\",\"JobModifyIndex\":1489,\"Meta\":null,\"ModifyIndex\":1489,\"Name\":\"job-10\",\"Namespace\":\"default\",\"ParameterizedJob\":null,\"ParentID\":\"\",\"Payload\":null,\"Periodic\":null,\"Priority\":50,\"Region\":\"global\",\"Stable\":false,\"Status\":\"dead\",\"StatusDescription\":\"\",\"Stop\":false,\"SubmitTime\":1535462270618419300,\"TaskGroups\":[{\"Constraints\":null,\"Count\":1,\"EphemeralDisk\":{\"Migrate\":false,\"SizeMB\":300,\"Sticky\":false},\"Meta\":null,\"Migrate\":null,\"Name\":\"ping\",\"ReschedulePolicy\":{\"Attempts\":1,\"Delay\":5000000000,\"DelayFunction\":\"constant\",\"Interval\":86400000000000,\"MaxDelay\":0,\"Unlimited\":false},\"RestartPolicy\":{\"Attempts\":3,\"Delay\":15000000000,\"Interval\":86400000000000,\"Mode\":\"fail\"},\"Tasks\":[{\"Artifacts\":null,\"Config\":{\"command\":\"ping\",\"args\":[\"8.8.8.8\",\"-n\",\"9\"]},\"Constraints\":null,\"DispatchPayload\":null,\"Driver\":\"raw_exec\",\"Env\":null,\"KillSignal\":\"\",\"KillTimeout\":5000000000,\"Leader\":false,\"LogConfig\":{\"MaxFileSizeMB\":10,\"MaxFiles\":10},\"Meta\":null,\"Name\":\"ping-google\",\"Resources\":{\"CPU\":100,\"DiskMB\":0,\"IOPS\":0,\"MemoryMB\":300,\"Networks\":null},\"Services\":null,\"ShutdownDelay\":0,\"Templates\":null,\"User\":\"\",\"Vault\":null}],\"Update\":null}],\"Type\":\"batch\",\"Update\":{\"AutoRevert\":false,\"Canary\":0,\"HealthCheck\":\"\",\"HealthyDeadline\":0,\"MaxParallel\":0,\"MinHealthyTime\":0,\"ProgressDeadline\":0,\"Stagger\":0},\"VaultToken\":\"\",\"Version\":0}]}";
            Uri job1Uri = new Uri($"http://127.0.0.1:4646/v1/job/{jobId}/versions");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            var expectedRequest = mockHttp.When(job1Uri.AbsoluteUri)
                    .Respond("application/json", versionsJson);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);

            api.Versions(jobId).GetAwaiter().GetResult();

            Assert.AreEqual(1, mockHttp.GetMatchCount(expectedRequest));
        }
        [TestMethod]
        public void Versions_ExistingJobId_ReturnsPopulatedResponse()
        {
            string jobId = "job-1";
            string versionsJson = "{\"Diffs\":null,\"Index\":3386,\"KnownLeader\":true,\"LastContact\":0,\"Versions\":[{\"AllAtOnce\":false,\"Constraints\":null,\"CreateIndex\":1489,\"Datacenters\":[\"dc1\"],\"Dispatched\":false,\"ID\":\"job-10\",\"JobModifyIndex\":3386,\"Meta\":null,\"ModifyIndex\":3386,\"Name\":\"job-10\",\"Namespace\":\"default\",\"ParameterizedJob\":null,\"ParentID\":\"\",\"Payload\":null,\"Periodic\":null,\"Priority\":50,\"Region\":\"global\",\"Stable\":false,\"Status\":\"dead\",\"StatusDescription\":\"\",\"Stop\":false,\"SubmitTime\":1535552347596677700,\"TaskGroups\":[{\"Constraints\":null,\"Count\":1,\"EphemeralDisk\":{\"Migrate\":false,\"SizeMB\":300,\"Sticky\":false},\"Meta\":null,\"Migrate\":null,\"Name\":\"ping\",\"ReschedulePolicy\":{\"Attempts\":1,\"Delay\":5000000000,\"DelayFunction\":\"constant\",\"Interval\":86400000000000,\"MaxDelay\":0,\"Unlimited\":false},\"RestartPolicy\":{\"Attempts\":3,\"Delay\":15000000000,\"Interval\":86400000000000,\"Mode\":\"fail\"},\"Tasks\":[{\"Artifacts\":null,\"Config\":{\"command\":\"ping\",\"args\":[\"8.8.8.8\",\"-n\",\"10\"]},\"Constraints\":null,\"DispatchPayload\":null,\"Driver\":\"raw_exec\",\"Env\":null,\"KillSignal\":\"\",\"KillTimeout\":5000000000,\"Leader\":false,\"LogConfig\":{\"MaxFileSizeMB\":10,\"MaxFiles\":10},\"Meta\":null,\"Name\":\"ping-google\",\"Resources\":{\"CPU\":100,\"DiskMB\":0,\"IOPS\":0,\"MemoryMB\":300,\"Networks\":null},\"Services\":null,\"ShutdownDelay\":0,\"Templates\":null,\"User\":\"\",\"Vault\":null}],\"Update\":null}],\"Type\":\"batch\",\"Update\":{\"AutoRevert\":false,\"Canary\":0,\"HealthCheck\":\"\",\"HealthyDeadline\":0,\"MaxParallel\":0,\"MinHealthyTime\":0,\"ProgressDeadline\":0,\"Stagger\":0},\"VaultToken\":\"\",\"Version\":1},{\"AllAtOnce\":false,\"Constraints\":null,\"CreateIndex\":1489,\"Datacenters\":[\"dc1\"],\"Dispatched\":false,\"ID\":\"job-10\",\"JobModifyIndex\":1489,\"Meta\":null,\"ModifyIndex\":1489,\"Name\":\"job-10\",\"Namespace\":\"default\",\"ParameterizedJob\":null,\"ParentID\":\"\",\"Payload\":null,\"Periodic\":null,\"Priority\":50,\"Region\":\"global\",\"Stable\":false,\"Status\":\"dead\",\"StatusDescription\":\"\",\"Stop\":false,\"SubmitTime\":1535462270618419300,\"TaskGroups\":[{\"Constraints\":null,\"Count\":1,\"EphemeralDisk\":{\"Migrate\":false,\"SizeMB\":300,\"Sticky\":false},\"Meta\":null,\"Migrate\":null,\"Name\":\"ping\",\"ReschedulePolicy\":{\"Attempts\":1,\"Delay\":5000000000,\"DelayFunction\":\"constant\",\"Interval\":86400000000000,\"MaxDelay\":0,\"Unlimited\":false},\"RestartPolicy\":{\"Attempts\":3,\"Delay\":15000000000,\"Interval\":86400000000000,\"Mode\":\"fail\"},\"Tasks\":[{\"Artifacts\":null,\"Config\":{\"command\":\"ping\",\"args\":[\"8.8.8.8\",\"-n\",\"9\"]},\"Constraints\":null,\"DispatchPayload\":null,\"Driver\":\"raw_exec\",\"Env\":null,\"KillSignal\":\"\",\"KillTimeout\":5000000000,\"Leader\":false,\"LogConfig\":{\"MaxFileSizeMB\":10,\"MaxFiles\":10},\"Meta\":null,\"Name\":\"ping-google\",\"Resources\":{\"CPU\":100,\"DiskMB\":0,\"IOPS\":0,\"MemoryMB\":300,\"Networks\":null},\"Services\":null,\"ShutdownDelay\":0,\"Templates\":null,\"User\":\"\",\"Vault\":null}],\"Update\":null}],\"Type\":\"batch\",\"Update\":{\"AutoRevert\":false,\"Canary\":0,\"HealthCheck\":\"\",\"HealthyDeadline\":0,\"MaxParallel\":0,\"MinHealthyTime\":0,\"ProgressDeadline\":0,\"Stagger\":0},\"VaultToken\":\"\",\"Version\":0}]}";
            Uri job1Uri = new Uri($"http://127.0.0.1:4646/v1/job/{jobId}/versions");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            mockHttp.When(job1Uri.AbsoluteUri)
                    .Respond("application/json", versionsJson);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);

            JobVersionsResponse versionResponse = api.Versions(jobId).GetAwaiter().GetResult();

            Assert.IsNotNull(versionResponse);
            Assert.IsNotNull(versionResponse.Versions);
            Assert.AreEqual(2, versionResponse.Versions.Count);
            Assert.IsTrue(versionResponse.Versions.TrueForAll(job => job.Id == "job-10"));
        }
        [TestMethod]
        [ExpectedException(typeof(EntityNotFound))]
        public void Versions_NonExistingJobId_ThrowsEntityNotFoundException()
        {
            string jobId = "job-1";

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);

            api.Versions(jobId).GetAwaiter().GetResult();
        }


        [TestMethod]
        public void Allocations_ExistingJobId_CallsCorrectUri()
        {
            string jobId = "job-1";
            string allocationsJson = "[{\"ClientDescription\":\"\",\"ClientStatus\":\"complete\",\"CreateIndex\":3388,\"CreateTime\":1535552347607677600,\"DeploymentStatus\":null,\"DesiredDescription\":\"\",\"DesiredStatus\":\"run\",\"DesiredTransition\":{\"ForceReschedule\":null,\"Migrate\":null,\"Reschedule\":null},\"EvalID\":\"9b4b21e9-ec77-f2ed-4387-2956fcf9035a\",\"FollowupEvalID\":\"\",\"ID\":\"30c262fc-ca53-9c12-fe3a-d1b8bdfdafb5\",\"JobID\":\"job-10\",\"JobVersion\":1,\"ModifyIndex\":3392,\"ModifyTime\":1535552358238684600,\"Name\":\"job-10.ping[0]\",\"NodeID\":\"575e1f00-0511-a48c-793e-96a29f0cd31f\",\"RescheduleTracker\":null,\"TaskGroup\":\"ping\",\"TaskStates\":{\"ping-google\":{\"Events\":[{\"Details\":{},\"DiskLimit\":0,\"DisplayMessage\":\"Task received by client\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535552347634677500,\"Type\":\"Received\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{\"message\":\"Building Task Directory\"},\"DiskLimit\":0,\"DisplayMessage\":\"Building Task Directory\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"Building Task Directory\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535552347635677500,\"Type\":\"Task Setup\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{},\"DiskLimit\":0,\"DisplayMessage\":\"Task started by client\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535552349112661000,\"Type\":\"Started\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{\"exit_code\":\"0\",\"signal\":\"0\"},\"DiskLimit\":0,\"DisplayMessage\":\"Exit Code: 0\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535552358212671500,\"Type\":\"Terminated\",\"ValidationError\":\"\",\"VaultError\":\"\"}],\"Failed\":false,\"FinishedAt\":\"2018-08-29T14:19:18.2156692Z\",\"LastRestart\":\"0001-01-01T00:00:00Z\",\"Restarts\":0,\"StartedAt\":\"2018-08-29T14:19:09.1276607Z\",\"State\":\"dead\"}}},{\"ClientDescription\":\"\",\"ClientStatus\":\"complete\",\"CreateIndex\":1491,\"CreateTime\":1535462270623417400,\"DeploymentStatus\":null,\"DesiredDescription\":\"\",\"DesiredStatus\":\"run\",\"DesiredTransition\":{\"ForceReschedule\":null,\"Migrate\":null,\"Reschedule\":null},\"EvalID\":\"38298f62-b376-f1d3-1b90-0387854114a6\",\"FollowupEvalID\":\"\",\"ID\":\"799d2513-9288-ca00-5611-e3cb4f418846\",\"JobID\":\"job-10\",\"JobVersion\":0,\"ModifyIndex\":1497,\"ModifyTime\":1535462280272119900,\"Name\":\"job-10.ping[0]\",\"NodeID\":\"575e1f00-0511-a48c-793e-96a29f0cd31f\",\"RescheduleTracker\":null,\"TaskGroup\":\"ping\",\"TaskStates\":{\"ping-google\":{\"Events\":[{\"Details\":{},\"DiskLimit\":0,\"DisplayMessage\":\"Task received by client\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535462270645419200,\"Type\":\"Received\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{\"message\":\"Building Task Directory\"},\"DiskLimit\":0,\"DisplayMessage\":\"Building Task Directory\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"Building Task Directory\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535462270647417700,\"Type\":\"Task Setup\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{},\"DiskLimit\":0,\"DisplayMessage\":\"Task started by client\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535462272152191700,\"Type\":\"Started\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{\"exit_code\":\"0\",\"signal\":\"0\"},\"DiskLimit\":0,\"DisplayMessage\":\"Exit Code: 0\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535462280220117900,\"Type\":\"Terminated\",\"ValidationError\":\"\",\"VaultError\":\"\"}],\"Failed\":false,\"FinishedAt\":\"2018-08-28T13:18:00.2211126Z\",\"LastRestart\":\"0001-01-01T00:00:00Z\",\"Restarts\":0,\"StartedAt\":\"2018-08-28T13:17:52.1661935Z\",\"State\":\"dead\"}}}]";
            Uri job1Uri = new Uri($"http://127.0.0.1:4646/v1/job/{jobId}/allocations");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            var expectedRequest = mockHttp.When(job1Uri.AbsoluteUri)
                    .Respond("application/json", allocationsJson);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);

            api.Allocations(jobId).GetAwaiter().GetResult();

            Assert.AreEqual(1, mockHttp.GetMatchCount(expectedRequest));
        }
        [TestMethod]
        public void Allocations_ExistingJobId_ReturnsPopulatedResponse()
        {
            string jobId = "job-1";
            string versionsJson = "[{\"ClientDescription\":\"\",\"ClientStatus\":\"complete\",\"CreateIndex\":3388,\"CreateTime\":1535552347607677600,\"DeploymentStatus\":null,\"DesiredDescription\":\"\",\"DesiredStatus\":\"run\",\"DesiredTransition\":{\"ForceReschedule\":null,\"Migrate\":null,\"Reschedule\":null},\"EvalID\":\"9b4b21e9-ec77-f2ed-4387-2956fcf9035a\",\"FollowupEvalID\":\"\",\"ID\":\"30c262fc-ca53-9c12-fe3a-d1b8bdfdafb5\",\"JobID\":\"job-10\",\"JobVersion\":1,\"ModifyIndex\":3392,\"ModifyTime\":1535552358238684600,\"Name\":\"job-10.ping[0]\",\"NodeID\":\"575e1f00-0511-a48c-793e-96a29f0cd31f\",\"RescheduleTracker\":null,\"TaskGroup\":\"ping\",\"TaskStates\":{\"ping-google\":{\"Events\":[{\"Details\":{},\"DiskLimit\":0,\"DisplayMessage\":\"Task received by client\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535552347634677500,\"Type\":\"Received\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{\"message\":\"Building Task Directory\"},\"DiskLimit\":0,\"DisplayMessage\":\"Building Task Directory\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"Building Task Directory\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535552347635677500,\"Type\":\"Task Setup\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{},\"DiskLimit\":0,\"DisplayMessage\":\"Task started by client\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535552349112661000,\"Type\":\"Started\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{\"exit_code\":\"0\",\"signal\":\"0\"},\"DiskLimit\":0,\"DisplayMessage\":\"Exit Code: 0\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535552358212671500,\"Type\":\"Terminated\",\"ValidationError\":\"\",\"VaultError\":\"\"}],\"Failed\":false,\"FinishedAt\":\"2018-08-29T14:19:18.2156692Z\",\"LastRestart\":\"0001-01-01T00:00:00Z\",\"Restarts\":0,\"StartedAt\":\"2018-08-29T14:19:09.1276607Z\",\"State\":\"dead\"}}},{\"ClientDescription\":\"\",\"ClientStatus\":\"complete\",\"CreateIndex\":1491,\"CreateTime\":1535462270623417400,\"DeploymentStatus\":null,\"DesiredDescription\":\"\",\"DesiredStatus\":\"run\",\"DesiredTransition\":{\"ForceReschedule\":null,\"Migrate\":null,\"Reschedule\":null},\"EvalID\":\"38298f62-b376-f1d3-1b90-0387854114a6\",\"FollowupEvalID\":\"\",\"ID\":\"799d2513-9288-ca00-5611-e3cb4f418846\",\"JobID\":\"job-10\",\"JobVersion\":0,\"ModifyIndex\":1497,\"ModifyTime\":1535462280272119900,\"Name\":\"job-10.ping[0]\",\"NodeID\":\"575e1f00-0511-a48c-793e-96a29f0cd31f\",\"RescheduleTracker\":null,\"TaskGroup\":\"ping\",\"TaskStates\":{\"ping-google\":{\"Events\":[{\"Details\":{},\"DiskLimit\":0,\"DisplayMessage\":\"Task received by client\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535462270645419200,\"Type\":\"Received\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{\"message\":\"Building Task Directory\"},\"DiskLimit\":0,\"DisplayMessage\":\"Building Task Directory\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"Building Task Directory\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535462270647417700,\"Type\":\"Task Setup\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{},\"DiskLimit\":0,\"DisplayMessage\":\"Task started by client\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535462272152191700,\"Type\":\"Started\",\"ValidationError\":\"\",\"VaultError\":\"\"},{\"Details\":{\"exit_code\":\"0\",\"signal\":\"0\"},\"DiskLimit\":0,\"DisplayMessage\":\"Exit Code: 0\",\"DownloadError\":\"\",\"DriverError\":\"\",\"DriverMessage\":\"\",\"ExitCode\":0,\"FailedSibling\":\"\",\"FailsTask\":false,\"GenericSource\":\"\",\"KillError\":\"\",\"KillReason\":\"\",\"KillTimeout\":0,\"Message\":\"\",\"RestartReason\":\"\",\"SetupError\":\"\",\"Signal\":0,\"StartDelay\":0,\"TaskSignal\":\"\",\"TaskSignalReason\":\"\",\"Time\":1535462280220117900,\"Type\":\"Terminated\",\"ValidationError\":\"\",\"VaultError\":\"\"}],\"Failed\":false,\"FinishedAt\":\"2018-08-28T13:18:00.2211126Z\",\"LastRestart\":\"0001-01-01T00:00:00Z\",\"Restarts\":0,\"StartedAt\":\"2018-08-28T13:17:52.1661935Z\",\"State\":\"dead\"}}}]";
            Uri job1Uri = new Uri($"http://127.0.0.1:4646/v1/job/{jobId}/allocations");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            mockHttp.When(job1Uri.AbsoluteUri)
                    .Respond("application/json", versionsJson);

            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);

            IList<Allocation> allocations = api.Allocations(jobId).GetAwaiter().GetResult();

            Assert.IsNotNull(allocations);
            Assert.AreEqual(2, allocations.Count);
        }
        [TestMethod]
        [ExpectedException(typeof(EntityNotFound))]
        public void Allocations_NonExistingJobId_ThrowsEntityNotFoundException()
        {
            string jobId = "job-1";

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();

            JobApi api = new JobApi(mockHttp.ToHttpClient(), apiConfig);

            api.Allocations(jobId).GetAwaiter().GetResult();
        }
    }
}
