using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Imposters
{
	/// <summary>
	/// Summary description for ImposterTests
	/// </summary>
	[TestClass, TestCategory("Unit")]
	public class HttpsImposterTests
	{
		#region Constructor Tests

		[TestMethod]
		public void HttpsImposter_Constructor_SetsPort()
		{
			const int port = 123;
			var imposter = new HttpsImposter(port, null, null);
			Assert.AreEqual(port, imposter.Port);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsProtocol()
		{
			var imposter = new HttpsImposter(123, null, null);
			Assert.AreEqual("https", imposter.Protocol);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsName()
		{
			const string expectedName = "Service";
			var imposter = new HttpsImposter(123, expectedName, null);
			Assert.AreEqual(expectedName, imposter.Name);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_AllowsNullPort()
		{
			var imposter = new HttpsImposter(null, null, null);
			Assert.AreEqual(default, imposter.Port);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_InitializesStubsCollection()
		{
			var imposter = new HttpsImposter(123, null, null);
			Assert.IsNotNull(imposter.Stubs);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsKey()
		{
			const string expectedKeyValue = "-----BEGIN CERTIFICATE-----base64_encoded_junk-----END CERTIFICATE-----";
			var imposter = new HttpsImposter(123, null, new HttpsImposterOptions { Key = expectedKeyValue });
			Assert.AreEqual(expectedKeyValue, imposter.Key);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsKeyAsNullWhenMissing()
		{
			var imposter = new HttpsImposter(123, null, null);
			Assert.IsNull(imposter.Key);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsCert()
		{
			const string expectedCertValue = "-----BEGIN CERTIFICATE-----base64_encoded_junk-----END CERTIFICATE-----";
			var imposter = new HttpsImposter(123, null, new HttpsImposterOptions { Cert = expectedCertValue });
			Assert.AreEqual(expectedCertValue, imposter.Cert);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsCertAsNullWhenMissing()
		{
			var imposter = new HttpsImposter(123, null, null);
			Assert.IsNull(imposter.Cert);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsMutualAuth()
		{
			const bool expectedMutualAuthRequired = true;
			var imposter = new HttpsImposter(123, null, new HttpsImposterOptions { MutualAuthRequired = expectedMutualAuthRequired });
			Assert.AreEqual(expectedMutualAuthRequired, imposter.MutualAuthRequired);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsMutualAuthFalseWhenMissing()
		{
			var imposter = new HttpsImposter(123, null, null);
			Assert.IsFalse(imposter.MutualAuthRequired);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsDefaultResponse()
		{
			var expectedDefaultResponse = new HttpResponseFields();
			var imposter = new HttpsImposter(123, null,
				new HttpsImposterOptions { DefaultResponse = expectedDefaultResponse });
			Assert.AreEqual(expectedDefaultResponse, imposter.DefaultResponse);
		}

		[TestMethod]
		public void Constructor_InitialAllowCORS()
		{
			var imposter = new HttpsImposter(null, null, null);
			Assert.IsFalse(imposter.AllowCORS);
		}

		[TestMethod]
		public void Constructor_AllowCORSTrue()
		{
			const bool expectedAllowCORS = true;
			var imposter = new HttpsImposter(null, null, new HttpsImposterOptions { AllowCORS = expectedAllowCORS });
			Assert.AreEqual(expectedAllowCORS, imposter.AllowCORS);
		}

		[TestMethod]
		public void Constructor_InitialRecordRequests()
		{
			const bool expectedRecordRequests = false;
			var imposter = new HttpsImposter(null, null, null);
			Assert.AreEqual(expectedRecordRequests, imposter.RecordRequests);
		}

		[TestMethod]
		public void Constructor_RecordRequestsTrue()
		{
			const bool expectedRecordRequests = true;
			var imposter = new HttpsImposter(null, null,
				new HttpsImposterOptions { RecordRequests = expectedRecordRequests });
			Assert.AreEqual(expectedRecordRequests, imposter.RecordRequests);
		}

		#endregion

		#region Stub Tests

		[TestMethod]
		public void HttpsImposter_AddStub_AddsStubToCollection()
		{
			var imposter = new HttpsImposter(123, null, null);
			imposter.AddStub();
			Assert.AreEqual(1, imposter.Stubs.Count);
		}

		#endregion
	}
}
