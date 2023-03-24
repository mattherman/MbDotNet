using System.Threading.Tasks;
using MbDotNet.Enums;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MbDotNet.Tests.Client
{
	[TestClass, TestCategory("Unit")]
	public class CreateTcpImposterAsyncTests : MountebankClientTestBase
	{
		[TestInitialize]
		public void Initialize()
		{
			MockRequestProxy
				.Setup(f => f.CreateImposterAsync(It.IsAny<TcpImposter>(), default))
				.Returns(Task.CompletedTask)
				.Verifiable();
		}

		[TestMethod]
		public async Task TcpImposter_Preconfigured()
		{
			var imposter = new TcpImposter(123, null, null);
			await Client.CreateTcpImposterAsync(imposter);
		}

		[TestMethod]
		public async Task TcpImposter_WithoutName_SetsNameToNull()
		{
			var imposter = await Client.CreateTcpImposterAsync(123, _ => { });

			Assert.IsNotNull(imposter);
			Assert.IsNull(imposter.Name);
		}

		[TestMethod]
		public async Task TcpImposter_WithName_SetsName()
		{
			const string expectedName = "Service";

			var imposter = await Client.CreateTcpImposterAsync(123, expectedName, _ => { });

			Assert.IsNotNull(imposter);
			Assert.AreEqual(expectedName, imposter.Name);
		}

		[TestMethod]
		public async Task TcpImposter_WithoutMode_SetsModeToText()
		{
			const TcpMode expectedMode = TcpMode.Text;

			var imposter = await Client.CreateTcpImposterAsync(123, _ => { });

			Assert.IsNotNull(imposter);
			Assert.AreEqual(expectedMode, imposter.Mode);
		}

		[TestMethod]
		public async Task TcpImposter_WithMode_SetsMode()
		{
			const TcpMode expectedMode = TcpMode.Binary;

			var imposter = await Client.CreateTcpImposterAsync(123, null, imposter => imposter.Mode = expectedMode);

			Assert.IsNotNull(imposter);
			Assert.AreEqual(expectedMode, imposter.Mode);
		}

		[TestMethod]
		public async Task TcpImposter_WithoutPortAndName_SetsPortAndNameToNull()
		{
			var imposter = await Client.CreateTcpImposterAsync(null, _ => { });

			Assert.IsNotNull(imposter);
			Assert.AreEqual(default, imposter.Port);
			Assert.IsNull(imposter.Name);
		}

		[TestMethod]
		public async Task TcpImposter_WithoutRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateTcpImposterAsync(null, _ => { });

			Assert.IsFalse(imposter.RecordRequests);
		}

		[TestMethod]
		public async Task TcpImposter_WithRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateTcpImposterAsync(null, imposter => imposter.RecordRequests = true);

			Assert.IsTrue(imposter.RecordRequests);
		}

		[TestMethod]
		public async Task HttpImposter_WithoutDefaultRequest_SetsDefaultRequest()
		{
			var imposter = await Client.CreateTcpImposterAsync(123, "service", _ => { });

			Assert.IsNull(imposter.DefaultResponse);
		}

		[TestMethod]
		public async Task HttpImposter_WithDefaultRequest_SetsDefaultRequest()
		{
			var defaultResponse = new TcpResponseFields();
			var imposter = await Client.CreateTcpImposterAsync(123, "service",
				imposter => imposter.DefaultResponse = defaultResponse);

			Assert.IsNotNull(imposter.DefaultResponse);
			Assert.AreEqual(defaultResponse, imposter.DefaultResponse);
		}
	}
}
