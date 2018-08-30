using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nomad.DotNet.UriUtilities;


namespace Nomad.DotNet.Tests.UriUtilities
{
    [TestClass]
    public class QueryStringBuilderTests
    {
        [TestMethod]
        public void AddField_ValidField_QueryStringPropHasCorrectValue()
        {
            QueryStringBuilder qsBuilder = new QueryStringBuilder();

            qsBuilder.AddField("field", "value");

            Assert.AreEqual("field=value", qsBuilder.QueryString);
        }

        [TestMethod]
        public void AddField_MultipleFields_QueryStringPropHasCorrectValue()
        {
            string ExpectedQueryString = "field1=value1&field2=value2&field3=value3";
            QueryStringBuilder qsBuilder = new QueryStringBuilder();

            qsBuilder.AddField("field1", "value1");
            qsBuilder.AddField("field2", "value2");
            qsBuilder.AddField("field3", "value3");

            Assert.AreEqual(ExpectedQueryString, qsBuilder.QueryString);
        }

        [TestMethod]
        public void RemoveField_OnlyField_QueryStringShouldBeEmptyString()
        {
            QueryStringBuilder qsBuilder = new QueryStringBuilder();
            qsBuilder.AddField("field1", "value1");

            qsBuilder.RemoveField("field1");

            Assert.AreEqual(string.Empty, qsBuilder.QueryString);
        }

        [TestMethod]
        public void RemoveField_MiddleField_QueryStringShouldBeCorrectString()
        {
            string ExpectedQueryString = "field1=value1&field3=value3";
            QueryStringBuilder qsBuilder = new QueryStringBuilder();
            qsBuilder.AddField("field1", "value1");
            qsBuilder.AddField("field2", "value2");
            qsBuilder.AddField("field3", "value3");

            qsBuilder.RemoveField("field2");

            Assert.AreEqual(ExpectedQueryString, qsBuilder.QueryString);
        }

        [TestMethod]
        public void RemoveField_NonExistingField_QueryStringShouldStayEmpty()
        {
            QueryStringBuilder qsBuilder = new QueryStringBuilder();

            qsBuilder.RemoveField("field2");

            Assert.AreEqual(string.Empty, qsBuilder.QueryString);
        }
    }
}
