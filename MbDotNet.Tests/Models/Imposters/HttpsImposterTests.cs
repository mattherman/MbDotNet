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
			var imposter = new HttpsImposter(port, null);
			Assert.AreEqual(port, imposter.Port);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsProtocol()
		{
			var imposter = new HttpsImposter(123, null);
			Assert.AreEqual("https", imposter.Protocol);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsName()
		{
			const string expectedName = "Service";
			var imposter = new HttpsImposter(123, expectedName);
			Assert.AreEqual(expectedName, imposter.Name);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_AllowsNullPort()
		{
			var imposter = new HttpsImposter(null, null);
			Assert.AreEqual(default(int), imposter.Port);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_InitializesStubsCollection()
		{
			var imposter = new HttpsImposter(123, null);
			Assert.IsNotNull(imposter.Stubs);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsKey()
		{
			const string expectedKeyValue = "-----BEGIN CERTIFICATE-----base64_encoded_junk-----END CERTIFICATE-----";
			var imposter = new HttpsImposter(123, null, expectedKeyValue, null, false);
			Assert.AreEqual(expectedKeyValue, imposter.Key);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsKeyAsNullWhenMissing()
		{
			var imposter = new HttpsImposter(123, null);
			Assert.IsNull(imposter.Key);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsCert()
		{
			const string expectedCertValue = "-----BEGIN CERTIFICATE-----base64_encoded_junk-----END CERTIFICATE-----";
			var imposter = new HttpsImposter(123, null, null, expectedCertValue, false);
			Assert.AreEqual(expectedCertValue, imposter.Cert);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsCertAsNullWhenMissing()
		{
			var imposter = new HttpsImposter(123, null);
			Assert.IsNull(imposter.Cert);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsMutualAuth()
		{
			var imposter = new HttpsImposter(123, null, null, null, true);
			Assert.IsTrue(imposter.MutualAuthRequired);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsMutualAuthFalseWhenMissing()
		{
			var imposter = new HttpsImposter(123, null);
			Assert.IsFalse(imposter.MutualAuthRequired);
		}

		[TestMethod]
		public void HttpsImposter_Constructor_SetsDefaultResponse()
		{
			var imposter = new HttpsImposter(123, null, defaultResponse: new HttpResponseFields());
			Assert.IsNotNull(imposter.DefaultResponse);
		}

		[TestMethod]
		public void Constructor_InitialAllowCORS()
		{
			const bool allowCORS = false;
			var imposter = new HttpsImposter(null, null);
			Assert.AreEqual(allowCORS, imposter.AllowCORS);
		}

		[TestMethod]
		public void Constructor_AllowCORSTrue()
		{
			const bool allowCORS = true;
			var imposter = new HttpsImposter(null, null, false, null, true);
			Assert.AreEqual(allowCORS, imposter.AllowCORS);
		}

		[TestMethod]
		public void Constructor_AllowCORSFalse()
		{
			const bool allowCORS = false;
			var imposter = new HttpsImposter(null, null, false, null, false);
			Assert.AreEqual(allowCORS, imposter.AllowCORS);
		}

		#endregion

		#region Stub Tests

		[TestMethod]
		public void HttpsImposter_AddStub_AddsStubToCollection()
		{
			var imposter = new HttpsImposter(123, null);
			imposter.AddStub();
			Assert.AreEqual(1, imposter.Stubs.Count);
		}

		#endregion
	}
}
