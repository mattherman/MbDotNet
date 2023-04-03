using MbDotNet.Models;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Imposters
{
	[TestClass, TestCategory("Unit")]
	public class TcpImposterTests
	{
		#region Constructor Tests

		[TestMethod]
		public void TcpImposter_Constructor_SetsPort()
		{
			const int port = 123;
			var imposter = new TcpImposter(port, null, null);
			Assert.AreEqual(port, imposter.Port);
		}

		[TestMethod]
		public void TcpImposter_Constructor_SetsProtocol()
		{
			var imposter = new TcpImposter(123, null, null);
			Assert.AreEqual("tcp", imposter.Protocol);
		}

		[TestMethod]
		public void TcpImposter_Constructor_SetsName()
		{
			const string expectedName = "Service";
			var imposter = new TcpImposter(123, expectedName, null);
			Assert.AreEqual(expectedName, imposter.Name);
		}

		[TestMethod]
		public void TcpImposter_Constructor_SetsMode()
		{
			const TcpMode expectedMode = TcpMode.Binary;
			var imposter = new TcpImposter(123, null, new TcpImposterOptions { Mode = expectedMode });
			Assert.AreEqual(expectedMode, imposter.Mode);
		}

		[TestMethod]
		public void TcpImposter_Constructor_AllowsNullPort()
		{
			var imposter = new TcpImposter(null, null, null);
			Assert.AreEqual(default, imposter.Port);
		}

		[TestMethod]
		public void TcpImposter_Constructor_InitializesStubsCollection()
		{
			var imposter = new TcpImposter(123, null, null);
			Assert.IsNotNull(imposter.Stubs);
		}

		[TestMethod]
		public void TcpImposter_Constructor_InitializesDefaultResponse()
		{
			var expectedDefaultResponse = new TcpResponseFields();
			var imposter = new TcpImposter(123, null,
				new TcpImposterOptions { DefaultResponse = expectedDefaultResponse });
			Assert.AreEqual(expectedDefaultResponse, imposter.DefaultResponse);
		}

		[TestMethod]
		public void Constructor_InitialRecordRequests()
		{
			const bool expectedRecordRequests = false;
			var imposter = new TcpImposter(null, null, null);
			Assert.AreEqual(expectedRecordRequests, imposter.RecordRequests);
		}

		[TestMethod]
		public void Constructor_RecordRequestsTrue()
		{
			const bool expectedRecordRequests = true;
			var imposter = new TcpImposter(null, null,
				new TcpImposterOptions { RecordRequests = expectedRecordRequests });
			Assert.AreEqual(expectedRecordRequests, imposter.RecordRequests);
		}

		#endregion

		#region Stub Tests

		[TestMethod]
		public void TcpImposter_AddStub_AddsStubToCollection()
		{
			var imposter = new TcpImposter(123, null, null);
			imposter.AddStub();
			Assert.AreEqual(1, imposter.Stubs.Count);
		}

		#endregion
	}
}
