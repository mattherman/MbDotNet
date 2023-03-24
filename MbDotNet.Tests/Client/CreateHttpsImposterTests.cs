using System;
using System.Threading.Tasks;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MbDotNet.Tests.Client
{
	[TestClass, TestCategory("Unit")]
	public class CreateHttpsImposterTests : MountebankClientTestBase
	{
		[TestInitialize]
		public void Initialize()
		{
			MockRequestProxy
				.Setup(f => f.CreateImposterAsync(It.IsAny<HttpsImposter>(), default))
				.Returns(Task.CompletedTask)
				.Verifiable();
		}

		[TestMethod]
		public async Task HttpsImposter_Preconfigured()
		{
			var imposter = new HttpsImposter(123, null, null);
			await Client.CreateHttpsImposterAsync(imposter);
		}

		[TestMethod]
		public async Task HttpsImposter_WithoutName_SetsNameToNull()
		{
			var imposter = await Client.CreateHttpsImposterAsync(123, _ => { });

			Assert.IsNotNull(imposter);
			Assert.IsNull(imposter.Name);
		}

		[TestMethod]
		public async Task HttpsImposter_WithName_SetsName()
		{
			const string expectedName = "Service";

			var imposter = await Client.CreateHttpsImposterAsync(123, expectedName, _ => { });

			Assert.IsNotNull(imposter);
			Assert.AreEqual(expectedName, imposter.Name);
		}

		[TestMethod]
		public async Task HttpsImposter_WithPEMFormattedKey_SetsKey()
		{
			const string expectedKey = "-----BEGIN CERTIFICATE-----base64_encoded_junk-----END CERTIFICATE-----";

			var imposter = await Client.CreateHttpsImposterAsync(123, imposter => imposter.Key = expectedKey);

			Assert.IsNotNull(imposter);
			Assert.AreEqual(expectedKey, imposter.Key);
		}

		[TestMethod]
		public async Task HttpsImposter_WithInvalidKey_ThrowsInvalidOperationException()
		{
			await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
				await Client.CreateHttpsImposterAsync(123, imposter => imposter.Key = "invalid"));
		}

		[TestMethod]
		public async Task HttpsImposter_WithPEMFormattedCert_SetsCert()
		{
			const string expectedCert = "-----BEGIN CERTIFICATE-----base64_encoded_junk-----END CERTIFICATE-----";

			var imposter = await Client.CreateHttpsImposterAsync(123, imposter => imposter.Cert = expectedCert);

			Assert.IsNotNull(imposter);
			Assert.AreEqual(expectedCert, imposter.Cert);
		}

		[TestMethod]
		public async Task HttpsImposter_WithInvalidCert_ThrowsInvalidOperationException()
		{
			await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
				await Client.CreateHttpsImposterAsync(123, imposter => imposter.Cert = "invalid"));
		}

		[TestMethod]
		public async Task HttpsImposter_WithoutPortAndName_SetsPortAndNameToNull()
		{
			var imposter = await Client.CreateHttpsImposterAsync(null, _ => { });

			Assert.IsNotNull(imposter);
			Assert.AreEqual(default, imposter.Port);
			Assert.IsNull(imposter.Name);
		}

		[TestMethod]
		public async Task HttpsImposter_WithoutRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateHttpsImposterAsync(null, _ => { });

			Assert.IsFalse(imposter.RecordRequests);
		}

		[TestMethod]
		public async Task HttpsImposter_WithRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateHttpsImposterAsync(null, imposter => imposter.RecordRequests = true);

			Assert.IsTrue(imposter.RecordRequests);
		}

		[TestMethod]
		public async Task HttpImposter_WithoutDefaultResponse_SetsDefaultResponse()
		{
			var imposter = await Client.CreateHttpsImposterAsync(123, "service", _ => { });

			Assert.IsNull(imposter.DefaultResponse);
		}

		[TestMethod]
		public async Task HttpImposter_WithDefaultResponse_SetsDefaultResponse()
		{
			var defaultResponse = new HttpResponseFields();
			var imposter = await Client.CreateHttpsImposterAsync(123, "service",
				imposter => imposter.DefaultResponse = defaultResponse);

			Assert.AreEqual(defaultResponse, imposter.DefaultResponse);
		}
	}
}
