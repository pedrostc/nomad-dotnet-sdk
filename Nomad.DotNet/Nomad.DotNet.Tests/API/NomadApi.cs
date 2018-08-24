using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nomad.DotNet.Model;
using System.Collections.Generic;
using Nomad.DotNet.API;
using System.Threading.Tasks;
using System.Net.Http;
using Moq;

namespace Nomad.DotNet.Tests.API
{
    [TestClass]
    public class NomadApiTests
    {
        [TestMethod]
        public void ToString_ValidObject_ReturnsValidJson()
        {
            Mock<HttpClient> mock = new Mock<HttpClient>();
            
            JobApi api = new JobApi(mock.Object);

            Job job = api.GetAsync("python-sum-12").GetAwaiter().GetResult();

            Assert.IsNotNull(job);
        }
    }
}
