using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Imposters
{
	/// <summary>
	/// Summary description for ImposterTests
	/// </summary>
	[TestClass, TestCategory("Unit")]
	public class HttpImposterTests
	{
		#region Constructor Tests

		[TestMethod]
		public void Constructor_SetsPort()
		{
			const int port = 123;
			var imposter = new HttpImposter(port, null, null);
			Assert.AreEqual(port, imposter.Port);
		}

		[TestMethod]
		public void Constructor_SetsProtocol()
		{
			var imposter = new HttpImposter(123, null, null);
			Assert.AreEqual("http", imposter.Protocol);
		}

		[TestMethod]
		public void Constructor_SetsName()
		{
			const string expectedName = "Service";
			var imposter = new HttpImposter(123, expectedName, null);
			Assert.AreEqual(expectedName, imposter.Name);
		}

		[TestMethod]
		public void Constructor_AllowsNullPort()
		{
			var imposter = new HttpImposter(null, null, null);
			Assert.AreEqual(default, imposter.Port);
		}

		[TestMethod]
		public void Constructor_InitializesStubsCollection()
		{
			var imposter = new HttpImposter(123, null, null);
			Assert.IsNotNull(imposter.Stubs);
		}

		[TestMethod]
		public void Constructor_InitialDefaultResponse()
		{
			var imposter = new HttpImposter(null, null, null);
			Assert.IsNull(imposter.DefaultResponse);
		}

		[TestMethod]
		public void Constructor_SetsDefaultResponse()
		{
			var expectedDefaultResponse = new HttpResponseFields();
			var imposter = new HttpImposter(123, null,
				new HttpImposterOptions { DefaultResponse = expectedDefaultResponse });
			Assert.AreEqual(expectedDefaultResponse, imposter.DefaultResponse);
		}

		[TestMethod]
		public void Constructor_InitialAllowCORS()
		{
			const bool expectedAllowCORS = false;
			var imposter = new HttpImposter(null, null, null);
			Assert.AreEqual(expectedAllowCORS, imposter.AllowCORS);
		}

		[TestMethod]
		public void Constructor_AllowCORSTrue()
		{
			const bool expectedAllowCORS = true;
			var imposter = new HttpImposter(null, null,
				new HttpImposterOptions { AllowCORS = expectedAllowCORS });
			Assert.AreEqual(expectedAllowCORS, imposter.AllowCORS);
		}

		[TestMethod]
		public void Constructor_InitialRecordRequests()
		{
			const bool expectedRecordRequests = false;
			var imposter = new HttpImposter(null, null, null);
			Assert.AreEqual(expectedRecordRequests, imposter.RecordRequests);
		}

		[TestMethod]
		public void Constructor_RecordRequestsTrue()
		{
			const bool expectedRecordRequests = true;
			var imposter = new HttpImposter(null, null,
				new HttpImposterOptions { RecordRequests = expectedRecordRequests });
			Assert.AreEqual(expectedRecordRequests, imposter.RecordRequests);
		}

		#endregion

		#region Stub Tests

		[TestMethod]
		public void AddStub_AddsStubToCollection()
		{
			var imposter = new HttpImposter(123, null, null);
			imposter.AddStub();
			Assert.AreEqual(1, imposter.Stubs.Count);
		}

		#endregion
	}
}
