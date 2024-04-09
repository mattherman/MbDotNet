using System.Threading.Tasks;
using MbDotNet.Models.Imposters;
using Moq;
using Xunit;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class CreateSmtpImposterAsyncTests : MountebankClientTestBase
	{
		[Fact]
		public void Initialize()
		{
			MockRequestProxy
				.Setup(f => f.CreateImposterAsync(It.IsAny<SmtpImposter>(), default))
				.Returns(Task.CompletedTask)
				.Verifiable();
		}

		[Fact]
		public async Task SmtpImposter_Preconfigured()
		{
			var imposter = new SmtpImposter(123, null, null);
			await Client.CreateSmtpImposterAsync(imposter);
		}

		[Fact]
		public async Task SmtpImposter_WithoutName_SetsNameToNull()
		{
			var imposter = await Client.CreateSmtpImposterAsync(123, _ => { });

			Assert.NotNull(imposter);
			Assert.Null(imposter.Name);
		}

		[Fact]
		public async Task SmtpImposter_WithName_SetsName()
		{
			const string expectedName = "Service";

			var imposter = await Client.CreateSmtpImposterAsync(123, expectedName, _ => { });

			Assert.NotNull(imposter);
			Assert.Equal(expectedName, imposter.Name);
		}

		[Fact]
		public async Task SmtpImposter_WithoutPortAndName_SetsPortAndNameToNull()
		{
			var imposter = await Client.CreateSmtpImposterAsync(null, _ => { });

			Assert.NotNull(imposter);
			Assert.Equal(default, imposter.Port);
			Assert.Null(imposter.Name);
		}

		[Fact]
		public async Task SmtpImposter_WithoutRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateSmtpImposterAsync(null, _ => { });

			Assert.False(imposter.RecordRequests);
		}

		[Fact]
		public async Task SmtpImposter_WithRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateSmtpImposterAsync(null, imposter => imposter.RecordRequests = true);

			Assert.True(imposter.RecordRequests);
		}
	}
}
