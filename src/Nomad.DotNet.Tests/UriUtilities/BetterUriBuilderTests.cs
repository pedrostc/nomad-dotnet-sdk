using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nomad.DotNet.UriUtilities;
using System;

namespace Nomad.DotNet.Tests.UriUtilities
{
    [TestClass]
    public class BetterUriBuilderTests
    {
        [TestMethod]
        public void Constructor_stringUri_UriPropShouldBeCorrect()
        {
            string uri = "http://127.0.0.1:8080/";

            BetterUriBuilder builder = new BetterUriBuilder(uri);

            Assert.AreEqual(uri, builder.Uri.AbsoluteUri);
        }
        [TestMethod]
        public void Constructor_stringUriWithPath_PathPropShouldBeCorrect()
        {
            string uri = "http://127.0.0.1:8080/path/v1";
            string expectedPath = "/path/v1";

            BetterUriBuilder builder = new BetterUriBuilder(uri);

            Assert.AreEqual(expectedPath, builder.Path);
        }

        [TestMethod]
        public void Constructor_Uri_UriPropShouldBeCorrect()
        {
            Uri uri = new Uri("http://127.0.0.1:8080/");

            BetterUriBuilder builder = new BetterUriBuilder(uri);

            Assert.AreEqual(uri, builder.Uri);
        }

        [TestMethod]
        public void Constructor_UriWithPath_PathPropShouldBeCorrect()
        {
            string expectedPath = "/path/v1";
            Uri uri = new Uri("http://127.0.0.1:8080/path/v1");

            BetterUriBuilder builder = new BetterUriBuilder(uri);

            Assert.AreEqual(expectedPath, builder.Path);
        }

        [TestMethod]
        public void Constructor_UriWithQueryString_QueryPropShouldBeCorrect()
        {
            string expectedQuery = "field1=value1&field2=value2";
            Uri uri = new Uri("http://127.0.0.1:8080/?field1=value1&field2=value2");

            BetterUriBuilder builder = new BetterUriBuilder(uri);

            Assert.AreEqual(expectedQuery, builder.Query);
        }

        [TestMethod]
        public void Constructor_SchemeHostName_UriPropShouldBeCorrect()
        {
            string scheme = "http";
            string hostName = "127.0.0.1";
            string expectedUri = "http://127.0.0.1/";

            BetterUriBuilder builder = new BetterUriBuilder(scheme, hostName);

            Assert.AreEqual(expectedUri, builder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void Constructor_SchemeHostNamePortNumber_UriPropShouldBeCorrect()
        {
            string scheme = "http";
            string hostName = "127.0.0.1";
            int portNumber = 8080;
            string expectedUri = "http://127.0.0.1:8080/";

            BetterUriBuilder builder = new BetterUriBuilder(scheme, hostName, portNumber);

            Assert.AreEqual(expectedUri, builder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void AddPathPart_SinglePath_PathPropIsSet()
        {
            BetterUriBuilder uriBuilder = new BetterUriBuilder();

            uriBuilder.AddPathPart("Path");

            Assert.IsNotNull(uriBuilder.Path);
            Assert.IsFalse(string.IsNullOrEmpty(uriBuilder.Path));
            Assert.IsFalse(string.IsNullOrWhiteSpace(uriBuilder.Path));
        }

        [TestMethod]
        public void AddPathPart_SinglePath_PathPropHasCorrectValue()
        {
            BetterUriBuilder uriBuilder = new BetterUriBuilder();

            uriBuilder.AddPathPart("Path");

            Assert.AreEqual("Path", uriBuilder.Path);
        }

        [TestMethod]
        public void AddPathPart_BaseUriWithPath_PathPropHasCorrectValue()
        {
            string uri = "http://127.0.0.1:8080/path/v1";
            string expectedPath = "/path/v1/job";
            BetterUriBuilder uriBuilder = new BetterUriBuilder(uri);

            uriBuilder.AddPathPart("job");

            Assert.AreEqual(expectedPath, uriBuilder.Path);
        }

        [TestMethod]
        public void AddPathPart_BaseUriWithPath_UriHasTheCorrectValue()
        {
            string uri = "http://127.0.0.1:8080/path/v1";
            string expectedUri = "http://127.0.0.1:8080/path/v1/job";
            BetterUriBuilder uriBuilder = new BetterUriBuilder(uri);

            uriBuilder.AddPathPart("job");

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void AddPathPart_MultipleParts_PathPropHasCorrectValue()
        {
            BetterUriBuilder uriBuilder = new BetterUriBuilder();

            uriBuilder.AddPathPart("Path");
            uriBuilder.AddPathPart("v1");
            uriBuilder.AddPathPart("Job");

            Assert.AreEqual("Path/v1/Job", uriBuilder.Path);
        }

        [TestMethod]
        public void AddPathPart_MultipleParts_UriPropHasCorrectValue()
        {
            string expectedUri = "http://127.0.0.1:8080/path/v1/job";
            BetterUriBuilder uriBuilder = new BetterUriBuilder("http://127.0.0.1:8080");

            uriBuilder.AddPathPart("path");
            uriBuilder.AddPathPart("v1");
            uriBuilder.AddPathPart("job");

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void AddPathPart_MultiplePartsWithNull_UriPropDoesNotContainNullPart()
        {
            string expectedUri = "http://127.0.0.1:8080/path//job";
            BetterUriBuilder uriBuilder = new BetterUriBuilder("http://127.0.0.1:8080");

            uriBuilder.AddPathPart("path");
            uriBuilder.AddPathPart(null);
            uriBuilder.AddPathPart("job");

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void AddPathPart_MultipleNullParts_UriPropDoesNotContainNullParts()
        {
            string expectedUri = "http://127.0.0.1:8080///job";
            BetterUriBuilder uriBuilder = new BetterUriBuilder("http://127.0.0.1:8080");

            uriBuilder.AddPathPart(null);
            uriBuilder.AddPathPart(null);
            uriBuilder.AddPathPart("job");

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void RemovePathPart_OnlyPart_PathIsRoot()
        {
            BetterUriBuilder uriBuilder = new BetterUriBuilder();

            uriBuilder.AddPathPart("Path");
            uriBuilder.RemovePathPart("Path");

            Assert.IsNotNull(uriBuilder.Path);
            Assert.AreEqual("/", uriBuilder.Path);
        }

        [TestMethod]
        public void RemovePathPart_MiddlePart_PathPropHasCorrectValue()
        {
            BetterUriBuilder uriBuilder = new BetterUriBuilder();

            uriBuilder.AddPathPart("Path");
            uriBuilder.AddPathPart("v1");
            uriBuilder.AddPathPart("Job");
            uriBuilder.RemovePathPart("v1");

            Assert.AreEqual("Path/Job", uriBuilder.Path);
        }

        [TestMethod]
        public void RemovePathPart_BaseUriWithPath_PathPropHasCorrectValue()
        {
            string uri = "http://127.0.0.1:8080/path/v1";
            string expectedPath = "/v1";
            BetterUriBuilder uriBuilder = new BetterUriBuilder(uri);

            uriBuilder.RemovePathPart("path");

            Assert.AreEqual(expectedPath, uriBuilder.Path);
        }

        [TestMethod]
        public void RemovePathPart_BaseUriWithPath_UriHasTheCorrectValue()
        {
            string uri = "http://127.0.0.1:8080/path/v1";
            string expectedUri = "http://127.0.0.1:8080/v1";
            BetterUriBuilder uriBuilder = new BetterUriBuilder(uri);

            uriBuilder.RemovePathPart("path");

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void ClearPath_NonPopulatedPath_PathPropIsRoot()
        {
            BetterUriBuilder uriBuilder = new BetterUriBuilder();

            uriBuilder.ClearPath();

            Assert.IsNotNull(uriBuilder.Path);
            Assert.AreEqual("/", uriBuilder.Path);
        }

        [TestMethod]
        public void ClearPath_BaseUriWithPath_PathPropHasCorrectValue()
        {
            string uri = "http://127.0.0.1:8080/path/v1";
            string expectedPath = "/";
            BetterUriBuilder uriBuilder = new BetterUriBuilder(uri);

            uriBuilder.ClearPath();

            Assert.AreEqual(expectedPath, uriBuilder.Path);
        }

        [TestMethod]
        public void ClearPath_BaseUriWithPath_UriHasTheCorrectValue()
        {
            string uri = "http://127.0.0.1:8080/path/v1";
            string expectedUri = "http://127.0.0.1:8080/";
            BetterUriBuilder uriBuilder = new BetterUriBuilder(uri);

            uriBuilder.ClearPath();

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void ContainsPath_ExistingPath_ReturnsTrue()
        {
            BetterUriBuilder uriBuilder = new BetterUriBuilder();

            uriBuilder.AddPathPart("Path");
            uriBuilder.AddPathPart("v1");
            uriBuilder.AddPathPart("Job");

            Assert.IsTrue(uriBuilder.ContainsPathPart("v1"));
        }

        [TestMethod]
        public void ContainsPath_NonExistingPath_ReturnsFalse()
        {
            BetterUriBuilder uriBuilder = new BetterUriBuilder();

            uriBuilder.AddPathPart("Path");

            Assert.IsFalse(uriBuilder.ContainsPathPart("v1"));
        }

        [TestMethod]
        public void ContainsPath_BaseUriPath_ReturnsTrue()
        {
            string uri = "http://127.0.0.1:8080/path/v1";
            BetterUriBuilder uriBuilder = new BetterUriBuilder(uri);

            Assert.IsTrue(uriBuilder.ContainsPathPart("v1"));
        }

        [TestMethod]
        public void AddQueryField_ValidField_UriShouldBeCorrect()
        {
            string expectedUri = "http://127.0.0.1:8080/?field=value";
            BetterUriBuilder uriBuilder = new BetterUriBuilder("http://127.0.0.1:8080");

            uriBuilder.AddQueryField("field", "value");

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void AddQueryField_MultipleFields_UriShouldBeCorrect()
        {
            string expectedUri = "http://127.0.0.1:8080/?field=value&other=value2";
            BetterUriBuilder uriBuilder = new BetterUriBuilder("http://127.0.0.1:8080");

            uriBuilder.AddQueryField("field", "value");
            uriBuilder.AddQueryField("other", "value2");

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void AddQueryField_BaseUriWithPath_QueryPropHasCorrectValue()
        {
            string expectedQuery = "field1=value1&field2=value2&field3=value3";
            Uri uri = new Uri("http://127.0.0.1:8080/?field1=value1&field2=value2");
            BetterUriBuilder uriBuilder = new BetterUriBuilder(uri);

            uriBuilder.AddQueryField("field3", "value3");

            Assert.AreEqual(expectedQuery, uriBuilder.Query);
        }

        [TestMethod]
        public void AddQueryField_BaseUriWithPath_UriHasCorrectValue()
        {
            string expectedUri = "http://127.0.0.1:8080/?field1=value1&field2=value2&field3=value3";
            Uri uri = new Uri("http://127.0.0.1:8080/?field1=value1&field2=value2");
            BetterUriBuilder uriBuilder = new BetterUriBuilder(uri);

            uriBuilder.AddQueryField("field3", "value3");

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void RemoveQueryField_SingleField_UriShouldBeCorrect()
        {
            string expectedUri = "http://127.0.0.1:8080/?other=value2";
            BetterUriBuilder uriBuilder = new BetterUriBuilder("http://127.0.0.1:8080");

            uriBuilder.AddQueryField("field", "value");
            uriBuilder.AddQueryField("other", "value2");

            uriBuilder.RemoveQueryField("field");

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void RemoveQueryField_BaseUriWithPath_QueryPropHasCorrectValue()
        {
            string expectedQuery = "field1=value1&field2=value2";
            Uri uri = new Uri("http://127.0.0.1:8080/?field1=value1&field2=value2&field3=value3");
            BetterUriBuilder uriBuilder = new BetterUriBuilder(uri);

            uriBuilder.RemoveQueryField("field3");

            Assert.AreEqual(expectedQuery, uriBuilder.Query);
        }

        [TestMethod]
        public void RemoveQueryField_BaseUriWithPath_UriHasCorrectValue()
        {
            string expectedUri = "http://127.0.0.1:8080/?field1=value1&field2=value2";
            Uri uri = new Uri("http://127.0.0.1:8080/?field1=value1&field2=value2&field3=value3");
            BetterUriBuilder uriBuilder = new BetterUriBuilder(uri);

            uriBuilder.RemoveQueryField("field3");

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void ClearQuery_ValidUri_UriShouldBeCorrect()
        {
            string expectedUri1 = "http://127.0.0.1:8080/?field=value&other=value2";
            string expectedUri = "http://127.0.0.1:8080/";
            BetterUriBuilder uriBuilder = new BetterUriBuilder("http://127.0.0.1:8080");

            uriBuilder.AddQueryField("field", "value");
            uriBuilder.AddQueryField("other", "value2");

            Assert.AreEqual(expectedUri1, uriBuilder.Uri.AbsoluteUri);

            uriBuilder.ClearQuery();

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void AddQueryFieldAddPathPart_ValidValues_UriShouldBeCorrect()
        {
            string expectedUri = "http://127.0.0.1:8080/v1/jobs?field=value&other=value2";
            BetterUriBuilder uriBuilder = new BetterUriBuilder("http://127.0.0.1:8080");

            uriBuilder.AddPathPart("v1");
            uriBuilder.AddPathPart("jobs");

            uriBuilder.AddQueryField("field", "value");
            uriBuilder.AddQueryField("other", "value2");

            Assert.AreEqual(expectedUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void ClearPath_ValidQueryString_UriShouldBeCorrect()
        {
            string fullUri = "http://127.0.0.1:8080/v1/jobs?field=value&other=value2";
            string onlyQueryUri = "http://127.0.0.1:8080/?field=value&other=value2";
            BetterUriBuilder uriBuilder = new BetterUriBuilder("http://127.0.0.1:8080");

            uriBuilder.AddPathPart("v1");
            uriBuilder.AddPathPart("jobs");

            uriBuilder.AddQueryField("field", "value");
            uriBuilder.AddQueryField("other", "value2");

            Assert.AreEqual(fullUri, uriBuilder.Uri.AbsoluteUri);

            uriBuilder.ClearPath();

            Assert.AreEqual(onlyQueryUri, uriBuilder.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void ClearQuery_ValidPath_UriShouldBeCorrect()
        {
            string fullUri = "http://127.0.0.1:8080/v1/jobs?field=value&other=value2";
            string onlyPathUri = "http://127.0.0.1:8080/v1/jobs";
            BetterUriBuilder uriBuilder = new BetterUriBuilder("http://127.0.0.1:8080");

            uriBuilder.AddPathPart("v1");
            uriBuilder.AddPathPart("jobs");

            uriBuilder.AddQueryField("field", "value");
            uriBuilder.AddQueryField("other", "value2");

            Assert.AreEqual(fullUri, uriBuilder.Uri.AbsoluteUri);

            uriBuilder.ClearQuery();

            Assert.AreEqual(onlyPathUri, uriBuilder.Uri.AbsoluteUri);
        }


    }
}
