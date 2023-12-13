using System;
using System.Threading.Tasks;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Moq;
using Xunit;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class CreateHttpsImposterTests : MountebankClientTestBase
	{
		[Fact]
		public void Initialize()
		{
			MockRequestProxy
				.Setup(f => f.CreateImposterAsync(It.IsAny<HttpsImposter>(), default))
				.Returns(Task.CompletedTask)
				.Verifiable();
		}

		[Fact]
		public async Task HttpsImposter_Preconfigured()
		{
			var imposter = new HttpsImposter(123, null, null);
			await Client.CreateHttpsImposterAsync(imposter);
		}

		[Fact]
		public async Task HttpsImposter_WithoutName_SetsNameToNull()
		{
			var imposter = await Client.CreateHttpsImposterAsync(123, _ => { });

			Assert.NotNull(imposter);
			Assert.Null(imposter.Name);
		}

		[Fact]
		public async Task HttpsImposter_WithName_SetsName()
		{
			const string expectedName = "Service";

			var imposter = await Client.CreateHttpsImposterAsync(123, expectedName, _ => { });

			Assert.NotNull(imposter);
			Assert.Equal(expectedName, imposter.Name);
		}

		[Fact]
		public async Task HttpsImposter_WithPEMFormattedKey_SetsKey()
		{
			const string expectedKey = "-----BEGIN CERTIFICATE-----base64_encoded_junk-----END CERTIFICATE-----";

			var imposter = await Client.CreateHttpsImposterAsync(123, imposter => imposter.Key = expectedKey);

			Assert.NotNull(imposter);
			Assert.Equal(expectedKey, imposter.Key);
		}

		[Fact]
		public async Task HttpsImposter_WithInvalidKey_ThrowsInvalidOperationException()
		{
			await Assert.ThrowsAsync<InvalidOperationException>(async () =>
				await Client.CreateHttpsImposterAsync(123, imposter => imposter.Key = "invalid"));
		}

		[Fact]
		public async Task HttpsImposter_WithPEMFormattedCert_SetsCert()
		{
			const string expectedCert = "-----BEGIN CERTIFICATE-----base64_encoded_junk-----END CERTIFICATE-----";

			var imposter = await Client.CreateHttpsImposterAsync(123, imposter => imposter.Cert = expectedCert);

			Assert.NotNull(imposter);
			Assert.Equal(expectedCert, imposter.Cert);
		}

		[Fact]
		public async Task HttpsImposter_WithInvalidCert_ThrowsInvalidOperationException()
		{
			await Assert.ThrowsAsync<InvalidOperationException>(async () =>
				await Client.CreateHttpsImposterAsync(123, imposter => imposter.Cert = "invalid"));
		}

		[Fact]
		public async Task HttpsImposter_WithoutPortAndName_SetsPortAndNameToNull()
		{
			var imposter = await Client.CreateHttpsImposterAsync(null, _ => { });

			Assert.NotNull(imposter);
			Assert.Equal(default, imposter.Port);
			Assert.Null(imposter.Name);
		}

		[Fact]
		public async Task HttpsImposter_WithoutRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateHttpsImposterAsync(null, _ => { });

			Assert.False(imposter.RecordRequests);
		}

		[Fact]
		public async Task HttpsImposter_WithRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateHttpsImposterAsync(null, imposter => imposter.RecordRequests = true);

			Assert.True(imposter.RecordRequests);
		}

		[Fact]
		public async Task HttpImposter_WithoutDefaultResponse_SetsDefaultResponse()
		{
			var imposter = await Client.CreateHttpsImposterAsync(123, "service", _ => { });

			Assert.Null(imposter.DefaultResponse);
		}

		[Fact]
		public async Task HttpImposter_WithDefaultResponse_SetsDefaultResponse()
		{
			var defaultResponse = new HttpResponseFields();
			var imposter = await Client.CreateHttpsImposterAsync(123, "service",
				imposter => imposter.DefaultResponse = defaultResponse);

			Assert.Equal(defaultResponse, imposter.DefaultResponse);
		}
	}
}
