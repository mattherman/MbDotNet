using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Acceptance
{
	[TestClass, TestCategory("Acceptance")]
	public class ConfigTests : AcceptanceTestBase
	{
		[TestInitialize]
		public async Task TestInitialize()
		{
			await _client.DeleteAllImpostersAsync();
		}

		[TestMethod]
		public async Task GetConfig()
		{
			var result = await _client.GetConfigAsync();
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Version);
			Assert.IsTrue(result.Process.Count > 0);
			Assert.IsTrue(result.Options.Count > 0);
		}
	}
}
