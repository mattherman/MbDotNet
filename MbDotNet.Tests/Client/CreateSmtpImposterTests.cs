using System.Threading.Tasks;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MbDotNet.Tests.Client
{
	[TestClass, TestCategory("Unit")]
	public class CreateSmtpImposterAsyncTests : MountebankClientTestBase
	{
		[TestInitialize]
		public void Initialize()
		{
			MockRequestProxy
				.Setup(f => f.CreateImposterAsync(It.IsAny<SmtpImposter>(), default))
				.Returns(Task.CompletedTask)
				.Verifiable();
		}

		[TestMethod]
		public async Task SmtpImposter_Preconfigured()
		{
			var imposter = new SmtpImposter(123, null, null);
			await Client.CreateSmtpImposterAsync(imposter);
		}

		[TestMethod]
		public async Task SmtpImposter_WithoutName_SetsNameToNull()
		{
			var imposter = await Client.CreateSmtpImposterAsync(123, _ => { });

			Assert.IsNotNull(imposter);
			Assert.IsNull(imposter.Name);
		}

		[TestMethod]
		public async Task SmtpImposter_WithName_SetsName()
		{
			const string expectedName = "Service";

			var imposter = await Client.CreateSmtpImposterAsync(123, expectedName, _ => { });

			Assert.IsNotNull(imposter);
			Assert.AreEqual(expectedName, imposter.Name);
		}

		[TestMethod]
		public async Task SmtpImposter_WithoutPortAndName_SetsPortAndNameToNull()
		{
			var imposter = await Client.CreateSmtpImposterAsync(null, _ => { });

			Assert.IsNotNull(imposter);
			Assert.AreEqual(default, imposter.Port);
			Assert.IsNull(imposter.Name);
		}

		[TestMethod]
		public async Task SmtpImposter_WithoutRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateSmtpImposterAsync(null, _ => { });

			Assert.IsFalse(imposter.RecordRequests);
		}

		[TestMethod]
		public async Task SmtpImposter_WithRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateSmtpImposterAsync(null, imposter => imposter.RecordRequests = true);

			Assert.IsTrue(imposter.RecordRequests);
		}
	}
}
