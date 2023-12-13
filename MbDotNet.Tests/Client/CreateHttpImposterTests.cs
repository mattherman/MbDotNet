using System.Threading.Tasks;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Moq;
using Xunit;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class CreateHttpImposterTests : MountebankClientTestBase
	{
		public CreateHttpImposterTests()
		{

		}

		[Fact]
		public void Initialize()
		{
			MockRequestProxy
				.Setup(f => f.CreateImposterAsync(It.IsAny<HttpImposter>(), default))
				.Returns(Task.CompletedTask)
				.Verifiable();
		}

		[Fact]
		public async Task HttpImposter_Preconfigured()
		{
			var imposter = new HttpImposter(123, null, null);
			await Client.CreateHttpImposterAsync(imposter);
		}

		[Fact]
		public async Task HttpImposter_WithoutName_SetsNameToNull()
		{
			var imposter = await Client.CreateHttpImposterAsync(123, _ => { });

			Assert.NotNull(imposter);
			Assert.Null(imposter.Name);
		}

		[Fact]
		public async Task HttpImposter_WithName_SetsName()
		{
			const string expectedName = "Service";

			var imposter = await Client.CreateHttpImposterAsync(123, expectedName, _ => { });

			Assert.NotNull(imposter);
			Assert.Equal(expectedName, imposter.Name);
		}

		[Fact]
		public async Task HttpImposter_WithoutPortAndName_SetsPortAndNameToNull()
		{
			var imposter = await Client.CreateHttpImposterAsync(null, _ => { });

			Assert.NotNull(imposter);
			Assert.Equal(default, imposter.Port);
			Assert.Null(imposter.Name);
		}

		[Fact]
		public async Task HttpImposter_WithoutRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateHttpImposterAsync(123, "service", _ => { });

			Assert.False(imposter.RecordRequests);
		}

		[Fact]
		public async Task HttpImposter_WithRecordRequests_SetsRecordRequest()
		{
			var imposter = await Client.CreateHttpImposterAsync(123, "service", imposter => imposter.RecordRequests = true);

			Assert.True(imposter.RecordRequests);
		}

		[Fact]
		public async Task HttpImposter_WithoutDefaultResponse_SetsDefaultResponse()
		{
			var imposter = await Client.CreateHttpImposterAsync(123, "service", _ => { });

			Assert.Null(imposter.DefaultResponse);
		}

		[Fact]
		public async Task HttpImposter_WithDefaultResponse_SetsDefaultResponse()
		{
			var defaultResponse = new HttpResponseFields();

			var imposter = await Client.CreateHttpImposterAsync(123, "service",
				imposter => imposter.DefaultResponse = defaultResponse);

			Assert.Equal(defaultResponse, imposter.DefaultResponse);
		}
	}
}
