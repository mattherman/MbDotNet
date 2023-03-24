using System.Threading.Tasks;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MbDotNet.Tests.Client
{
	[TestClass, TestCategory("Unit")]
	public class CreateHttpImposterTests : MountebankClientTestBase
	{
		[TestInitialize]
		public void Initialize()
		{
			MockRequestProxy
				.Setup(f => f.CreateImposterAsync(It.IsAny<HttpImposter>(), default))
				.Returns(Task.CompletedTask)
				.Verifiable();
		}

		[TestMethod]
		public async Task HttpImposter_Preconfigured()
		{
			var imposter = new HttpImposter(123, null, null);
			await Client.CreateHttpImposterAsync(imposter);
		}

		[TestMethod]
		public async Task HttpImposter_WithoutName_SetsNameToNull()
		{
			var imposter = await Client.CreateHttpImposterAsync(123, _ => { });

			Assert.IsNotNull(imposter);
			Assert.IsNull(imposter.Name);
		}

		[TestMethod]
		public async Task HttpImposter_WithName_SetsName()
		{
			const string expectedName = "Service";

			var imposter = await Client.CreateHttpImposterAsync(123, expectedName, _ => { });

			Assert.IsNotNull(imposter);
			Assert.AreEqual(expectedName, imposter.Name);
		}

		[TestMethod]
		public async Task HttpImposter_WithoutPortAndName_SetsPortAndNameToNull()
		{
			var imposter = await Client.CreateHttpImposterAsync(null, _ => { });

			Assert.IsNotNull(imposter);
			Assert.AreEqual(default, imposter.Port);
			Assert.IsNull(imposter.Name);
		}

		[TestMethod]
		public async Task HttpImposter_WithoutRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateHttpImposterAsync(123, "service", _ => { });

			Assert.IsFalse(imposter.RecordRequests);
		}

		[TestMethod]
		public async Task HttpImposter_WithRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateHttpImposterAsync(123, "service", imposter => imposter.RecordRequests = true);

			Assert.IsTrue(imposter.RecordRequests);
		}

		[TestMethod]
		public async Task HttpImposter_WithoutDefaultResponse_SetsDefaultResponse()
		{
			var imposter = await Client.CreateHttpImposterAsync(123, "service", _ => { });

			Assert.IsNull(imposter.DefaultResponse);
		}

		[TestMethod]
		public async Task HttpImposter_WithDefaultResponse_SetsDefaultResponse()
		{
			var defaultResponse = new HttpResponseFields();

			var imposter = await Client.CreateHttpImposterAsync(123, "service",
				imposter => imposter.DefaultResponse = defaultResponse);

			Assert.AreEqual(defaultResponse, imposter.DefaultResponse);
		}
	}
}
