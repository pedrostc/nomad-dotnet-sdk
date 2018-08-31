using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nomad.DotNet.API;
using RichardSzalay.MockHttp;
using System;

namespace Nomad.DotNet.Tests.API
{
    [TestClass]
    public class NomadApiTests
    {
        NomadApiConfig apiConfig = new NomadApiConfig
        {
            HostUri = "http://127.0.0.1:4646"
        };

        [TestMethod]
        public void GetWithType_ValidRequest_ObjectInstance()
        {
            string fileContent = @"{ key: ""value"" }";
            Uri jobUri = new Uri("http://127.0.0.1:4646/v1/path");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            var expectedRequest = mockHttp.When(jobUri.AbsoluteUri)
                    .Respond("application/json", fileContent);

            NomadApiStub api = new NomadApiStub(mockHttp.ToHttpClient(), apiConfig);

            dynamic response = api.GetWithType().GetAwaiter().GetResult();

            Assert.IsNotNull(response);
        }
    }
}
