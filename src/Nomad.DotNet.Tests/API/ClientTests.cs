using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nomad.DotNet.API;
using RichardSzalay.MockHttp;
using System;

namespace Nomad.DotNet.Tests.API
{
    [TestClass]
    public class ClientTests
    {
        NomadApiConfig apiConfig = new NomadApiConfig
        {
            HostUri = "http://127.0.0.1:4646"
        };

        [TestMethod]
        public void ReadFile_ValidRequest_FileContent()
        {
            string allocationId = "123456";
            string fileContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Uri jobUri = new Uri("http://127.0.0.1:4646/v1/client/fs/cat/123456");

            MockHttpMessageHandler mockHttp = new MockHttpMessageHandler();
            var expectedRequest = mockHttp.When(jobUri.AbsoluteUri)
                    .Respond("text/plain", fileContent);

            Client api = new Client(mockHttp.ToHttpClient(), apiConfig);

            string response = api.ReadFile(allocationId).GetAwaiter().GetResult();

            Assert.AreEqual(fileContent, response);
        }
    }
}
