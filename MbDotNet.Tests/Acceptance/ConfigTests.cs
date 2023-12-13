using System.Threading.Tasks;

using Xunit;

namespace MbDotNet.Tests.Acceptance
{
	[Trait("Category", "Acceptance")]
	[Collection("Sequential")]
	public class ConfigTests : AcceptanceTestBase
	{
		/// <summary>
		/// It act as test initialize in x unit
		/// at https://xunit.net/docs/comparisons
		/// </summary>
		public ConfigTests()
		{
			_client.DeleteAllImpostersAsync().ConfigureAwait(false);
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
