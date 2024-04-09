using System.Threading.Tasks;

using Xunit;

namespace MbDotNet.Tests.Acceptance

{
	[Trait("Category", "Acceptance")]
	[Collection("Sequential")]
	public class ResponseTests : AcceptanceTestBase, IAsyncLifetime
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
		public async Task CanGetEntryHypermedia()
		{
			var result = await _client.GetEntryHypermediaAsync();
			Assert.NotNull(result);
		}

		[Fact]
		public async Task CanGetLogs()
		{
			var result = await _client.GetLogsAsync();
			Assert.NotNull(result);
		}
	}
}

