using System.Threading.Tasks;

using Xunit;

namespace MbDotNet.Tests.Acceptance
{
	[Trait("Category", "Acceptance")]
	[Collection("Sequential")]
	public class ConfigTests : AcceptanceTestBase, IAsyncLifetime
	{
		public async Task InitializeAsync()
		{
			await _client.DeleteAllImpostersAsync();
		}

		public Task DisposeAsync()
		{
			return Task.CompletedTask;
		}

		[Fact]
		public async Task GetConfig()
		{
			var result = await _client.GetConfigAsync();
			Assert.NotNull(result);
			Assert.NotNull(result.Version);
			Assert.True(result.Process.Count > 0);
			Assert.True(result.Options.Count > 0);
		}
	}
}

