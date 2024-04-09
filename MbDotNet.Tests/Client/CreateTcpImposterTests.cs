using System.Threading.Tasks;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Moq;
using Xunit;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class CreateTcpImposterAsyncTests : MountebankClientTestBase
	{
		[Fact]
		public void Initialize()
		{
			MockRequestProxy
				.Setup(f => f.CreateImposterAsync(It.IsAny<TcpImposter>(), default))
				.Returns(Task.CompletedTask)
				.Verifiable();
		}

		[Fact]
		public async Task TcpImposter_Preconfigured()
		{
			var imposter = new TcpImposter(123, null, null);
			await Client.CreateTcpImposterAsync(imposter);
		}

		[Fact]
		public async Task TcpImposter_WithoutName_SetsNameToNull()
		{
			var imposter = await Client.CreateTcpImposterAsync(123, _ => { });

			Assert.NotNull(imposter);
			Assert.Null(imposter.Name);
		}

		[Fact]
		public async Task TcpImposter_WithName_SetsName()
		{
			const string expectedName = "Service";

			var imposter = await Client.CreateTcpImposterAsync(123, expectedName, _ => { });

			Assert.NotNull(imposter);
			Assert.Equal(expectedName, imposter.Name);
		}

		[Fact]
		public async Task TcpImposter_WithoutMode_SetsModeToText()
		{
			const TcpMode expectedMode = TcpMode.Text;

			var imposter = await Client.CreateTcpImposterAsync(123, _ => { });

			Assert.NotNull(imposter);
			Assert.Equal(expectedMode, imposter.Mode);
		}

		[Fact]
		public async Task TcpImposter_WithMode_SetsMode()
		{
			const TcpMode expectedMode = TcpMode.Binary;

			var imposter = await Client.CreateTcpImposterAsync(123, null, imposter => imposter.Mode = expectedMode);

			Assert.NotNull(imposter);
			Assert.Equal(expectedMode, imposter.Mode);
		}

		[Fact]
		public async Task TcpImposter_WithoutPortAndName_SetsPortAndNameToNull()
		{
			var imposter = await Client.CreateTcpImposterAsync(null, _ => { });

			Assert.NotNull(imposter);
			Assert.Equal(default, imposter.Port);
			Assert.Null(imposter.Name);
		}

		[Fact]
		public async Task TcpImposter_WithoutRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateTcpImposterAsync(null, _ => { });

			Assert.False(imposter.RecordRequests);
		}

		[Fact]
		public async Task TcpImposter_WithRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateTcpImposterAsync(null, imposter => imposter.RecordRequests = true);

			Assert.True(imposter.RecordRequests);
		}

		[Fact]
		public async Task HttpImposter_WithoutDefaultRequest_SetsDefaultRequest()
		{
			var imposter = await Client.CreateTcpImposterAsync(123, "service", _ => { });

			Assert.Null(imposter.DefaultResponse);
		}

		[Fact]
		public async Task HttpImposter_WithDefaultRequest_SetsDefaultRequest()
		{
			var defaultResponse = new TcpResponseFields();
			var imposter = await Client.CreateTcpImposterAsync(123, "service",
				imposter => imposter.DefaultResponse = defaultResponse);

			Assert.NotNull(imposter.DefaultResponse);
			Assert.Equal(defaultResponse, imposter.DefaultResponse);
		}
	}
}
