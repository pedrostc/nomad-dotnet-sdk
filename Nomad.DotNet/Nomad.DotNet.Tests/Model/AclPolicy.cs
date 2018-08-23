using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nomad.DotNet.Model;
using System.Collections.Generic;

namespace Nomad.DotNet.Tests
{
    [TestClass]
    public class AclPolicyTests
    {
        [TestMethod]
        public void ToString_ValidObject_ReturnsValidJson()
        {
            string expectedJson = "{\"Name\":\"Test\",\"Description\":\"Test\",\"Rules\":\"namespace 'default' { policy = 'read' }\",\"CreateIndex\":0,\"ModifyIndex\":0}";
            AclPolicy aclPolicy = new AclPolicy()
                {
                    Name = "Test",
                    Description = "Test",
                    Rules = "namespace 'default' { policy = 'read' }"
                };

            string json = aclPolicy.ToString();

            Assert.IsNotNull(json);
            Assert.AreEqual(expectedJson, json);
        }
        [TestMethod]
        public void FromJsonArray_ValidJsonArray_ReturnsValidList()
        {
            string inputJson = "[{\"Name\":\"Test\",\"Description\":\"Test\",\"Rules\":\"namespace 'default' { policy = 'read' }\",\"CreateIndex\":0,\"ModifyIndex\":0}," +
                "{\"Name\":\"Test\",\"Description\":\"Test\",\"Rules\":\"namespace 'default' { policy = 'read' }\",\"CreateIndex\":0,\"ModifyIndex\":0},"+
                "{\"Name\":\"Test\",\"Description\":\"Test\",\"Rules\":\"namespace 'default' { policy = 'read' }\",\"CreateIndex\":0,\"ModifyIndex\":0}]";

            IList<AclPolicy> resultList = AclPolicy.FromJsonArray(inputJson);

            Assert.IsNotNull(resultList);
            Assert.AreEqual(3, resultList.Count);
        }
    }
}
