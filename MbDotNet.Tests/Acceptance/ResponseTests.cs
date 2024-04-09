using System.Threading.Tasks;

using Xunit;

namespace MbDotNet.Tests.Acceptance

{
	[Trait("Category", "Acceptance")]
	[Collection("Sequential")]
	public class ResponseTests : AcceptanceTestBase
	{
		/// <summary>
		/// It act as test initialize in x unit
		/// at https://xunit.net/docs/comparisons
		/// </summary>
		public ResponseTests()
		{
			_client.DeleteAllImpostersAsync().ConfigureAwait(false);
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
