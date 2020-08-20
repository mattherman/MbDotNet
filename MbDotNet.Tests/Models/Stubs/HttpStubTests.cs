using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Stubs
{
    [TestClass, TestCategory("Unit")]
    public class HttpStubTests
    {
        [TestMethod]
        public void HttpStub_Constructor_InitializesResponsesCollection()
        {
            var stub = new HttpStub();
            Assert.IsNotNull(stub.Responses);
        }

        [TestMethod]
        public void HttpStub_Constructor_InitializesPredicatesCollection()
        {
            var stub = new HttpStub();
            Assert.IsNotNull(stub.Predicates);
        }

        [TestMethod]
        public void HttpStub_ReturnsStatus_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            var stub = new HttpStub();
            stub.ReturnsStatus(expectedStatusCode);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatusCode, response.Fields.StatusCode);
        }

        [TestMethod]
        public void HttpStub_Returns_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            var headers = new Dictionary<string, object> { {"Content-Type", "application/json"} };

            var stub = new HttpStub();
            stub.Returns(expectedStatusCode, headers, "test");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatusCode, response.Fields.StatusCode);
        }

        [TestMethod]
        public void HttpStub_Returns_AddsResponse_LatencySet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            const int expectedLatencyInMilliseconds = 1000;
            var headers = new Dictionary<string, object> { { "Content-Type", "application/json" } };

            var stub = new HttpStub();
            stub.Returns(expectedStatusCode, headers, "test", latencyInMilliseconds: expectedLatencyInMilliseconds);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Behavior);
            Assert.IsNotNull(response.Behavior.LatencyInMilliseconds);
            Assert.AreEqual(expectedLatencyInMilliseconds, response.Behavior.LatencyInMilliseconds);
        }

        [TestMethod]
        public void HttpStub_Returns_AddsResponse_ResponseObjectSet()
        {
            const string expectedResponseObject = "Test Response";
            var headers = new Dictionary<string, object> { { "Content-Type", "application/json" } };

            var stub = new HttpStub();
            stub.Returns(HttpStatusCode.OK, headers, expectedResponseObject);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedResponseObject, response.Fields.ResponseObject);
        }

        [TestMethod]
        public void HttpStub_Returns_AddsResponse_ContentTypeHeaderSet()
        {
            var headers = new Dictionary<string, object> { { "Content-Type", "application/json" } };

            var stub = new HttpStub();
            stub.Returns(HttpStatusCode.OK, headers, "test");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(headers, response.Fields.Headers);
        }

        [TestMethod]
        public void HttpStub_Returns_AddsResponse()
        {
            var expectedResponse = new IsResponse<HttpResponseFields>(new HttpResponseFields());

            var stub = new HttpStub();
            stub.Returns(expectedResponse);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.AreEqual(expectedResponse, response);
        }

        [TestMethod]
        public void HttpStub_Returns_AddsBehavior()
        {
            var expectedResponse = new IsResponse<HttpResponseFields>(new HttpResponseFields(), new Behavior());

            var stub = new HttpStub();
            stub.Returns(expectedResponse);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.AreEqual(expectedResponse.Behavior, response?.Behavior);
        }

        [TestMethod]
        public void HttpStub_ReturnsBody_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            var stub = new HttpStub();
            stub.ReturnsBody(expectedStatusCode, "test");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatusCode, response.Fields.StatusCode);
        }

        [TestMethod]
        public void HttpStub_ReturnsBody_AddsResponse_ResponseObjectSet()
        {
            const string expectedBody = "test";

            var stub = new HttpStub();
            stub.ReturnsBody(HttpStatusCode.OK, expectedBody);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedBody, response.Fields.ResponseObject.ToString());
        }

        [TestMethod]
        public void HttpStub_ReturnsXml_AddsResponse_StatusCodeSet()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;

            var stub = new HttpStub();
            stub.ReturnsXml(expectedStatusCode, "test");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedStatusCode, response.Fields.StatusCode);
        }

        [TestMethod]
        public void HttpStub_ReturnsXml_AddsResponse_ResponseObjectSerializedAndSet()
        {
            const string expectedResponseObject = "<string>Test Response</string>";

            var stub = new HttpStub();
            stub.ReturnsXml(HttpStatusCode.OK, "Test Response");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Fields.ResponseObject.ToString().Contains(expectedResponseObject));
        }

        [TestMethod]
        public void HttpStub_ReturnsXml_AddsResponse_DefaultsToUtf8Encoding()
        {
            const string expectedEncoding = "utf-8";

            var stub = new HttpStub();
            stub.ReturnsXml(HttpStatusCode.OK, "Test Response");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Fields.ResponseObject.ToString().Contains(expectedEncoding));
        }

        [TestMethod]
        public void HttpStub_ReturnsXmlWithEncoding_AddsResponse_WithSpecifiedEncoding()
        {
            const string expectedEncoding = "utf-16";

            var stub = new HttpStub();
            stub.ReturnsXml(HttpStatusCode.OK, "Test Response", Encoding.Unicode);

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Fields.ResponseObject.ToString().Contains(expectedEncoding));
        }

        [TestMethod]
        public void HttpStub_ReturnsXml_AddsResponse_ContentTypeHeaderSet()
        {
            var headers = new Dictionary<string, string> { { "Content-Type", "application/xml" } };

            var stub = new HttpStub();
            stub.ReturnsXml(HttpStatusCode.OK, "test");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(headers["Content-Type"], response.Fields.Headers["Content-Type"]);
        }
        
        [TestMethod]
        public void HttpStub_ReturnsBinary_AddsResponse_ContentTypePdf()
        {
            var stub = new HttpStub();
            var bytes = new byte[] {3, 3, 5, 2, 23, 5, 21, 1};
            stub.ReturnsBinary(HttpStatusCode.OK, bytes, "application/pdf");

            var response = stub.Responses.First() as IsResponse<HttpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual("application/pdf", response.Fields.Headers["Content-Type"]);
        }

        [TestMethod]
        public void HttpStub_OnPathEquals_AddsPredicate_PathSet()
        {
            const string expectedPath = "/test";

            var stub = new HttpStub();
            stub.OnPathEquals(expectedPath);

            var predicate = stub.Predicates.First() as EqualsPredicate<HttpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedPath, predicate.Fields.Path);
        }

        [TestMethod]
        public void HttpStub_OnMethodEquals_AddsPredicate_MethodSet()
        {
            const Method expectedMethod = Method.Post;

            var stub = new HttpStub();
            stub.OnMethodEquals(expectedMethod);

            var predicate = stub.Predicates.First() as EqualsPredicate<HttpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedMethod, predicate.Fields.Method);
        }

        [TestMethod]
        public void HttpStub_OnPathAndMethodEqual_AddsPredicate_PathSet()
        {
            const string expectedPath = "/test";

            var stub = new HttpStub();
            stub.OnPathAndMethodEqual(expectedPath, Method.Get);

            var predicate = stub.Predicates.First() as EqualsPredicate<HttpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedPath, predicate.Fields.Path);
        }

        [TestMethod]
        public void HttpStub_OnPathAndMethodEqual_AddsPredicate_MethodSet()
        {
            const Method expectedMethod = Method.Post;

            var stub = new HttpStub();
            stub.OnPathAndMethodEqual("/test", expectedMethod);

            var predicate = stub.Predicates.First() as EqualsPredicate<HttpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedMethod, predicate.Fields.Method);
        }

        [TestMethod]
        public void HttpStub_On_AddsPredicate()
        {
            var expectedPredicate = new EqualsPredicate<HttpPredicateFields>(new HttpPredicateFields());

            var stub = new HttpStub();
            stub.On(expectedPredicate);

            var predicate = stub.Predicates.First() as EqualsPredicate<HttpPredicateFields>;
            Assert.AreEqual(expectedPredicate, predicate);
        }

        [TestMethod]
        public void HttpStub_ReturnsProxy_ReturnsHttpBooleanProxyStub()
        {            
            var predicateInvokingProxyStub = new ContainsPredicate<HttpPredicateFields>(new HttpPredicateFields
            {
                Path = "/aTestPath"
            });

            var proxyGeneratorPredicate = new MatchesPredicate<HttpBooleanPredicateFields>(new HttpBooleanPredicateFields
            {
                Path = true,
                Method = true,
                QueryParameters = true
            });

            var proxyToUrl = new Uri("http://someTestDestination.com");
            var proxyModeToUse = ProxyMode.ProxyTransparent;

            var stub = new HttpStub();
            stub.On(predicateInvokingProxyStub)
                .ReturnsProxy(proxyToUrl, proxyModeToUse, new []{ proxyGeneratorPredicate });

            var proxyResponse = stub.Responses.First() as ProxyResponse<ProxyResponseFields<HttpBooleanPredicateFields>>;

            Assert.AreEqual(proxyToUrl, proxyResponse.Fields.To);
            Assert.AreEqual(proxyModeToUse, proxyResponse.Fields.Mode);
            Assert.AreEqual(proxyGeneratorPredicate, proxyResponse.Fields.PredicateGenerators.First());
        }

        [TestMethod]
        public void HttpStub_ReturnsProxy_ReturnsHttpProxyStub()
        {            
            var predicateInvokingProxyStub = new ContainsPredicate<HttpPredicateFields>(new HttpPredicateFields
            {
                Path = "/aTestPath"
            });

            var proxyGeneratorPredicate = new MatchesPredicate<HttpPredicateFields>(new HttpPredicateFields
            {
                Path = "/aTestPath",
                Method = Method.Get,
                QueryParameters = new Dictionary<string, object> { { "q", "value" }}
            });

            var proxyToUrl = new Uri("http://someTestDestination.com");
            var proxyModeToUse = ProxyMode.ProxyTransparent;

            var stub = new HttpStub();
            stub.On(predicateInvokingProxyStub)
                .ReturnsProxy(proxyToUrl, proxyModeToUse, new []{ proxyGeneratorPredicate });

            var proxyResponse = stub.Responses.First() as ProxyResponse<ProxyResponseFields<HttpPredicateFields>>;

            Assert.AreEqual(proxyToUrl, proxyResponse.Fields.To);
            Assert.AreEqual(proxyModeToUse, proxyResponse.Fields.Mode);
            Assert.AreEqual(proxyGeneratorPredicate, proxyResponse.Fields.PredicateGenerators.First());
        }

        [TestMethod]
        public void HttpStub_InjectedFunction_AddsPredicate_InjectedFunctionSet()
        {
            const string injectedFunction = "function(config) { return true; }";

            var stub = new HttpStub();
            stub.OnInjectedFunction(injectedFunction);

            var predicate = stub.Predicates.First() as InjectPredicate;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(injectedFunction, predicate.InjectedFunction);
        }
    }
}
