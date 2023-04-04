using System;
using System.Linq;
using MbDotNet.Models;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		public void TcpStub_Returns_AddsResponse_BehaviorSet()
		{
			var expectedFields = new TcpResponseFields
			{
				Data = "TestData"
			};
			const int expectedLatencyInMilliseconds = 1000;

			var behavior = new WaitBehavior(expectedLatencyInMilliseconds);

			var stub = new TcpStub();
			stub.Returns(new IsResponse<TcpResponseFields>(expectedFields, new []{ behavior }));

			var response = stub.Responses.First() as IsResponse<TcpResponseFields>;
			Assert.IsNotNull(response?.Behaviors);
			Assert.AreEqual(1, response.Behaviors.Count);
			var responseBehavior = response.Behaviors[0] as WaitBehavior;
			Assert.AreSame(behavior, responseBehavior);
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
			const ProxyMode proxyModeToUse = ProxyMode.ProxyTransparent;

			var stub = new TcpStub();
			stub.On(predicateInvokingProxyStub)
				.ReturnsProxy(proxyToUrl, proxyModeToUse, new[] { proxyGeneratorPredicate });

			var proxyResponse = stub.Responses.First() as ProxyResponse<ProxyResponseFields<TcpBooleanPredicateFields>>;

			Assert.IsNotNull(proxyResponse);
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
			const ProxyMode proxyModeToUse = ProxyMode.ProxyTransparent;

			var stub = new TcpStub();
			stub.On(predicateInvokingProxyStub)
				.ReturnsProxy(proxyToUrl, proxyModeToUse, new[] { proxyGeneratorPredicate });

			var proxyResponse = stub.Responses.First() as ProxyResponse<ProxyResponseFields<TcpPredicateFields>>;

			Assert.IsNotNull(proxyResponse);
			Assert.AreEqual(proxyToUrl, proxyResponse.Fields.To);
			Assert.AreEqual(proxyModeToUse, proxyResponse.Fields.Mode);
			Assert.AreEqual(proxyGeneratorPredicate, proxyResponse.Fields.PredicateGenerators.First());
		}

		[TestMethod]
		public void TcpStub_ReturnsFault_ReturnsStubWithFaultResponse()
		{
			const Fault expectedFault = Fault.ConnectionResetByPeer;
			var stub = new HttpStub();
			stub.ReturnsFault(expectedFault);

			var faultResponse = stub.Responses[0] as FaultResponse;

			Assert.IsNotNull(faultResponse);
			Assert.AreEqual(expectedFault, faultResponse.Fault);
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
