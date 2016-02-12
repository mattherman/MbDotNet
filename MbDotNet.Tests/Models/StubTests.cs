using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models
{
    [TestClass]
    public class StubTests
    {
        [TestMethod]
        public void Constructor_InitializesResponsesCollection()
        {
            var stub = new Stub();
            Assert.IsNotNull(stub.Responses);
        }

        [TestMethod]
        public void Constructor_InitializesPredicatesCollection()
        {
            var stub = new Stub();
            Assert.IsNotNull(stub.Predicates);
        }

        [TestMethod]
        public void ReturnsStatus_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            var stub = new Stub();
            stub.ReturnsStatus(expectedStatusCode);

            var response = stub.Responses.First() as IsResponse;
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [TestMethod]
        public void ReturnsJson_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            var stub = new Stub();
            stub.ReturnsJson(expectedStatusCode, "test");

            var response = stub.Responses.First() as IsResponse;
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [TestMethod]
        public void ReturnsJson_AddsResponse_ResponseObjectSet()
        {
            const string expectedResponseObject = "Test Response";

            var stub = new Stub();
            stub.ReturnsJson(HttpStatusCode.OK, expectedResponseObject);

            var response = stub.Responses.First() as IsResponse;
            Assert.AreEqual(expectedResponseObject, response.ResponseObject);
        }

        [TestMethod]
        public void ReturnsJson_AddsResponse_ContentTypeHeaderSet()
        {
            var stub = new Stub();
            stub.ReturnsJson(HttpStatusCode.OK, "test");

            var response = stub.Responses.First() as IsResponse;
            Assert.AreEqual("application/json", response.Headers["Content-Type"]);
        }

        [TestMethod]
        public void ReturnsXml_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            var stub = new Stub();
            stub.ReturnsXml(expectedStatusCode, "test");

            var response = stub.Responses.First() as IsResponse;
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [TestMethod]
        public void ReturnsXml_AddsResponse_ResponseObjectSerializedAndSet()
        {
            const string expectedResponseObject = "<string>Test Response</string>";

            var stub = new Stub();
            stub.ReturnsXml(HttpStatusCode.OK, "Test Response");

            var response = stub.Responses.First() as IsResponse;
            Assert.IsTrue(response.ResponseObject.ToString().Contains(expectedResponseObject));
        }

        [TestMethod]
        public void ReturnsXml_AddsResponse_ContentTypeHeaderSet()
        {
            var stub = new Stub();
            stub.ReturnsXml(HttpStatusCode.OK, "test");

            var response = stub.Responses.First() as IsResponse;
            Assert.AreEqual("application/xml", response.Headers["Content-Type"]);
        }

        [TestMethod]
        public void Returns_AddsResponse()
        {
            var expectedResponse = new IsResponse(HttpStatusCode.OK, "Test Response",
                new Dictionary<string, string> {{"Content-Type", "application/json"}});

            var stub = new Stub();
            stub.Returns(expectedResponse);

            var response = stub.Responses.First() as IsResponse;
            Assert.AreEqual(expectedResponse, response);
        }
    }
}
