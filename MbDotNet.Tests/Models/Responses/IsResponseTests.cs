using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Responses
{
    [TestClass]
    public class IsResponseTests
    {
        [TestMethod]
        public void Constructor_OneParameter_SetsStatusCode()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.Created;

            var response = new IsResponse(expectedStatusCode);
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [TestMethod]
        public void Constructor_TwoParameters_SetsStatusCode()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.Created;

            var response = new IsResponse(expectedStatusCode, null);
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [TestMethod]
        public void Constructor_SingleParameter_SetsResponseObject()
        {
            const string expectedResponseObject = "Test";

            var response = new IsResponse(HttpStatusCode.Created, expectedResponseObject);
            Assert.AreEqual(expectedResponseObject, response.ResponseObject);
        }
    }
}
