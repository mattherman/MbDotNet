using System.Collections.Generic;
using System.Net;
using MbDotNet.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Responses
{
    [TestClass]
    public class IsResponseTests
    {
        [TestMethod]
        public void Constructor_SetsStatusCode()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.Created;

            var response = new IsResponse(expectedStatusCode, null, null);
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [TestMethod]
        public void Constructor_SetsResponseObject()
        {
            const string expectedResponseObject = "Test";

            var response = new IsResponse(HttpStatusCode.Created, expectedResponseObject, null);
            Assert.AreEqual(expectedResponseObject, response.ResponseObject);
        }

        [TestMethod]
        public void Constructor_SetsHeaders()
        {
            var expectedHeaders = new Dictionary<string, string>
            {
                {"Content-Type", "application/json"}
            };

            var response = new IsResponse(HttpStatusCode.Created, null, expectedHeaders);
            Assert.AreEqual(expectedHeaders, response.Headers);
        }
    }
}
