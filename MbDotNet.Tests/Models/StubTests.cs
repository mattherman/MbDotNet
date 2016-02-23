using System.Collections.Generic;
using System.Linq;
using System.Net;
using MbDotNet.Enums;
using MbDotNet.Models;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Responses;
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
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [TestMethod]
        public void Returns_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            var headers = new Dictionary<string, string> { {"Content-Type", "application/json"} };

            var stub = new Stub();
            stub.Returns(expectedStatusCode, headers, "test");

            var response = stub.Responses.First() as IsResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [TestMethod]
        public void Returns_AddsResponse_ResponseObjectSet()
        {
            const string expectedResponseObject = "Test Response";
            var headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };

            var stub = new Stub();
            stub.Returns(HttpStatusCode.OK, headers, expectedResponseObject);

            var response = stub.Responses.First() as IsResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedResponseObject, response.ResponseObject);
        }

        [TestMethod]
        public void Returns_AddsResponse_ContentTypeHeaderSet()
        {
            var headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };

            var stub = new Stub();
            stub.Returns(HttpStatusCode.OK, headers, "test");

            var response = stub.Responses.First() as IsResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(headers, response.Headers);
        }

        [TestMethod]
        public void Returns_AddsResponse()
        {
            var headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };
            var expectedResponse = new IsResponse(HttpStatusCode.OK, "Test Response", headers);

            var stub = new Stub();
            stub.Returns(expectedResponse);

            var response = stub.Responses.First() as IsResponse;
            Assert.AreEqual(expectedResponse, response);
        }

        [TestMethod]
        public void ReturnsXml_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            var stub = new Stub();
            stub.ReturnsXml(expectedStatusCode, "test");

            var response = stub.Responses.First() as IsResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
        }

        [TestMethod]
        public void ReturnsXml_AddsResponse_ResponseObjectSerializedAndSet()
        {
            const string expectedResponseObject = "<string>Test Response</string>";

            var stub = new Stub();
            stub.ReturnsXml(HttpStatusCode.OK, "Test Response");

            var response = stub.Responses.First() as IsResponse;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.ResponseObject.ToString().Contains(expectedResponseObject));
        }

        [TestMethod]
        public void ReturnsXml_AddsResponse_ContentTypeHeaderSet()
        {
            var headers = new Dictionary<string, string> { { "Content-Type", "application/xml" } };

            var stub = new Stub();
            stub.ReturnsXml(HttpStatusCode.OK, "test");

            var response = stub.Responses.First() as IsResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(headers["Content-Type"], response.Headers["Content-Type"]);
        }

        [TestMethod]
        public void OnPathEquals_AddsPredicate_PathSet()
        {
            const string expectedPath = "/test";

            var stub = new Stub();
            stub.OnPathEquals("/test");

            var predicate = stub.Predicates.First() as EqualsPredicate;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedPath, predicate.Path);
        }

        [TestMethod]
        public void OnPathAndMethodEqual_AddsPredicate_PathSet()
        {
            const string expectedPath = "/test";

            var stub = new Stub();
            stub.OnPathAndMethodEqual("/test", Method.Get);

            var predicate = stub.Predicates.First() as EqualsPredicate;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedPath, predicate.Path);
        }

        [TestMethod]
        public void OnPathAndMethodEqual_AddsPredicate_MethodSet()
        {
            const string expectedMethodString = "POST";

            var stub = new Stub();
            stub.OnPathAndMethodEqual("/test", Method.Post);

            var predicate = stub.Predicates.First() as EqualsPredicate;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedMethodString, predicate.Method);
        }

        [TestMethod]
        public void On_AddsPredicate()
        {
            var headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };
            var expectedPredicate = new EqualsPredicate("/test", Method.Get, "test", headers, null);

            var stub = new Stub();
            stub.On(expectedPredicate);

            var predicate = stub.Predicates.First() as EqualsPredicate;
            Assert.AreEqual(expectedPredicate, predicate);
        }
    }
}
