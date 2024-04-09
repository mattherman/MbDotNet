using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Xunit;

namespace MbDotNet.Tests.Models.Imposters
{
	/// <summary>
	/// Summary description for ImposterTests
	/// </summary>
	[Trait("Category", "Unit")]
	public class HttpsImposterTests
	{
		#region Constructor Tests

		[Fact]
		public void HttpsImposter_Constructor_SetsPort()
		{
			const int port = 123;
			var imposter = new HttpsImposter(port, null, null);
			Assert.Equal(port, imposter.Port);
		}

		[Fact]
		public void HttpsImposter_Constructor_SetsProtocol()
		{
			var imposter = new HttpsImposter(123, null, null);
			Assert.Equal("https", imposter.Protocol);
		}

		[Fact]
		public void HttpsImposter_Constructor_SetsName()
		{
			const string expectedName = "Service";
			var imposter = new HttpsImposter(123, expectedName, null);
			Assert.Equal(expectedName, imposter.Name);
		}

		[Fact]
		public void HttpsImposter_Constructor_AllowsNullPort()
		{
			var imposter = new HttpsImposter(null, null, null);
			Assert.Equal(default, imposter.Port);
		}

		[Fact]
		public void HttpsImposter_Constructor_InitializesStubsCollection()
		{
			var imposter = new HttpsImposter(123, null, null);
			Assert.NotNull(imposter.Stubs);
		}

		[Fact]
		public void HttpsImposter_Constructor_SetsKey()
		{
			const string expectedKeyValue = "-----BEGIN CERTIFICATE-----base64_encoded_junk-----END CERTIFICATE-----";
			var imposter = new HttpsImposter(123, null, new HttpsImposterOptions { Key = expectedKeyValue });
			Assert.Equal(expectedKeyValue, imposter.Key);
		}

		[Fact]
		public void HttpsImposter_Constructor_SetsKeyAsNullWhenMissing()
		{
			var imposter = new HttpsImposter(123, null, null);
			Assert.Null(imposter.Key);
		}

		[Fact]
		public void HttpsImposter_Constructor_SetsCert()
		{
			const string expectedCertValue = "-----BEGIN CERTIFICATE-----base64_encoded_junk-----END CERTIFICATE-----";
			var imposter = new HttpsImposter(123, null, new HttpsImposterOptions { Cert = expectedCertValue });
			Assert.Equal(expectedCertValue, imposter.Cert);
		}

		[Fact]
		public void HttpsImposter_Constructor_SetsCertAsNullWhenMissing()
		{
			var imposter = new HttpsImposter(123, null, null);
			Assert.Null(imposter.Cert);
		}

		[Fact]
		public void HttpsImposter_Constructor_SetsMutualAuth()
		{
			const bool expectedMutualAuthRequired = true;
			var imposter = new HttpsImposter(123, null, new HttpsImposterOptions { MutualAuthRequired = expectedMutualAuthRequired });
			Assert.Equal(expectedMutualAuthRequired, imposter.MutualAuthRequired);
		}

		[Fact]
		public void HttpsImposter_Constructor_SetsMutualAuthFalseWhenMissing()
		{
			var imposter = new HttpsImposter(123, null, null);
			Assert.False(imposter.MutualAuthRequired);
		}

		[Fact]
		public void HttpsImposter_Constructor_SetsDefaultResponse()
		{
			var expectedDefaultResponse = new HttpResponseFields();
			var imposter = new HttpsImposter(123, null,
				new HttpsImposterOptions { DefaultResponse = expectedDefaultResponse });
			Assert.Equal(expectedDefaultResponse, imposter.DefaultResponse);
		}

		[Fact]
		public void Constructor_InitialAllowCORS()
		{
			var imposter = new HttpsImposter(null, null, null);
			Assert.False(imposter.AllowCORS);
		}

		[Fact]
		public void Constructor_AllowCORSTrue()
		{
			const bool expectedAllowCORS = true;
			var imposter = new HttpsImposter(null, null, new HttpsImposterOptions { AllowCORS = expectedAllowCORS });
			Assert.Equal(expectedAllowCORS, imposter.AllowCORS);
		}

		[Fact]
		public void Constructor_InitialRecordRequests()
		{
			const bool expectedRecordRequests = false;
			var imposter = new HttpsImposter(null, null, null);
			Assert.Equal(expectedRecordRequests, imposter.RecordRequests);
		}

		[Fact]
		public void Constructor_RecordRequestsTrue()
		{
			const bool expectedRecordRequests = true;
			var imposter = new HttpsImposter(null, null,
				new HttpsImposterOptions { RecordRequests = expectedRecordRequests });
			Assert.Equal(expectedRecordRequests, imposter.RecordRequests);
		}

		#endregion

		#region Stub Tests

		[Fact]
		public void HttpsImposter_AddStub_AddsStubToCollection()
		{
			var imposter = new HttpsImposter(123, null, null);
			imposter.AddStub();
			Assert.Single(imposter.Stubs);
		}

		#endregion
	}
}
