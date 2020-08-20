using System.Linq;
using MbDotNet.Models.Stubs;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MbDotNet.Enums;
using System;

namespace MbDotNet.Tests.Models.Stubs
{
    [TestClass, TestCategory("Unit")]
    public class TcpStubTests
    {
        [TestMethod]
        public void TcpStub_Constructor_InitializesResponsesCollection()
        {
            var stub = new TcpStub();
            Assert.IsNotNull(stub.Responses);
        }

        [TestMethod]
        public void TcpStub_Constructor_InitializesPredicatesCollection()
        {
            var stub = new TcpStub();
            Assert.IsNotNull(stub.Predicates);
        }

        [TestMethod]
        public void TcpStub_ReturnsData_AddsResponse_DataSet()
        {
            const string expectedData = "TestData";

            var stub = new TcpStub();
            stub.ReturnsData(expectedData);

            var response = stub.Responses.First() as IsResponse<TcpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedData, response.Fields.Data);
        }

        [TestMethod]
        public void TcpStub_Returns_AddsResponse_SetsFields()
        {
            var expectedFields = new TcpResponseFields
            {
                Data = "TestData"
            };

            var stub = new TcpStub();
            stub.Returns(new IsResponse<TcpResponseFields>(expectedFields));

            var response = stub.Responses.First() as IsResponse<TcpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedFields, response.Fields);
        }

        [TestMethod]
        public void TcpStub_Returns_AddsResponse_LatencySet()
        {
            var expectedFields = new TcpResponseFields
            {
                Data = "TestData"
            };
            const int expectedLatencyInMilliseconds = 1000;

            var behavior = new Behavior
            {
                LatencyInMilliseconds = expectedLatencyInMilliseconds
            };

            var stub = new TcpStub();
            stub.Returns(new IsResponse<TcpResponseFields>(expectedFields, behavior));

            var response = stub.Responses.First() as IsResponse<TcpResponseFields>;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Behavior);
            Assert.IsNotNull(response.Behavior.LatencyInMilliseconds);
            Assert.AreEqual(expectedLatencyInMilliseconds, response.Behavior.LatencyInMilliseconds);
        }
        
        [TestMethod]
        public void TcpStub_Returns_AddsResponse_BehaviorSet()
        {
            var expectedFields = new TcpResponseFields
            {
                Data = "TestData"
            };

            var behavior = new Behavior();

            var stub = new TcpStub();
            stub.Returns(new IsResponse<TcpResponseFields>(expectedFields, behavior));

            var response = stub.Responses.First() as IsResponse<TcpResponseFields>;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Behavior);
            Assert.AreEqual(behavior, response.Behavior);
        }

        [TestMethod]
        public void TcpStub_OnDataEquals_AddsPredicate_DataSet()
        {
            const string expectedData = "TestData";

            var stub = new TcpStub();
            stub.OnDataEquals(expectedData);

            var predicate = stub.Predicates.First() as EqualsPredicate<TcpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedData, predicate.Fields.Data);
        }

        [TestMethod]
        public void TcpStub_On_AddsPredicate_SetsFields()
        {
            var expectedFields = new TcpPredicateFields
            {
                Data = "TestData",
                RequestFrom = "socket"
            };

            var stub = new TcpStub();
            stub.On(new EqualsPredicate<TcpPredicateFields>(expectedFields));

            var predicate = stub.Predicates.First() as EqualsPredicate<TcpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedFields, predicate.Fields);
        }

        [TestMethod]
        public void TcpStub_ReturnsProxy_ReturnsTcpBooleanProxyStub()
        {            
            var predicateInvokingProxyStub = new ContainsPredicate<TcpPredicateFields>(new TcpPredicateFields
            {
                Data = "123345"
            });

            var proxyGeneratorPredicate = new MatchesPredicate<TcpBooleanPredicateFields>(new TcpBooleanPredicateFields
            {
                Data = true
            });

            var proxyToUrl = new Uri("tcp://someTestDestination.com");
            var proxyModeToUse = ProxyMode.ProxyTransparent;

            var stub = new TcpStub();
            stub.On(predicateInvokingProxyStub)
                .ReturnsProxy(proxyToUrl, proxyModeToUse, new []{ proxyGeneratorPredicate });

            var proxyResponse = stub.Responses.First() as ProxyResponse<ProxyResponseFields<TcpBooleanPredicateFields>>;

            Assert.AreEqual(proxyToUrl, proxyResponse.Fields.To);
            Assert.AreEqual(proxyModeToUse, proxyResponse.Fields.Mode);
            Assert.AreEqual(proxyGeneratorPredicate, proxyResponse.Fields.PredicateGenerators.First());
        }

        [TestMethod]
        public void TcpStub_ReturnsProxy_ReturnsTcpProxyStub()
        {            
            var predicateInvokingProxyStub = new ContainsPredicate<TcpPredicateFields>(new TcpPredicateFields
            {
                Data = "123345"
            });

            var proxyGeneratorPredicate = new MatchesPredicate<TcpPredicateFields>(new TcpPredicateFields
            {
                Data = "123345"
            });

            var proxyToUrl = new Uri("tcp://someTestDestination.com");
            var proxyModeToUse = ProxyMode.ProxyTransparent;

            var stub = new TcpStub();
            stub.On(predicateInvokingProxyStub)
                .ReturnsProxy(proxyToUrl, proxyModeToUse, new []{ proxyGeneratorPredicate });

            var proxyResponse = stub.Responses.First() as ProxyResponse<ProxyResponseFields<TcpPredicateFields>>;

            Assert.AreEqual(proxyToUrl, proxyResponse.Fields.To);
            Assert.AreEqual(proxyModeToUse, proxyResponse.Fields.Mode);
            Assert.AreEqual(proxyGeneratorPredicate, proxyResponse.Fields.PredicateGenerators.First());
        }

        [TestMethod]
        public void TcpStub_InjectedFunction_AddsPredicate_InjectedFunctionSet()
        {
            const string injectedFunction = "function(config) { return true; }";

            var stub = new TcpStub();
            stub.OnInjectedFunction(injectedFunction);

            var predicate = stub.Predicates.First() as InjectPredicate;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(injectedFunction, predicate.InjectedFunction);
        }
    }
}
