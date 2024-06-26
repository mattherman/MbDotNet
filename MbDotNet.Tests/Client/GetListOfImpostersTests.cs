using System.Collections.Generic;
using System.Threading.Tasks;
using MbDotNet.Models.Imposters;
using Moq;
using Xunit;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class GetListOfImpostersTests : MountebankClientTestBase
	{
		[Fact]
		public async Task ImpostersRetrieved_ReturnsImposters()
		{
			await Client.DeleteAllImpostersAsync();

			var expectedImposters = new List<SimpleRetrievedImposter>
			{
				new SimpleRetrievedImposter { Port=123, Protocol = "Http" },
				new SimpleRetrievedImposter { Port=456, Protocol = "Http" }
			};
			MockRequestProxy.Setup(x => x.GetImpostersAsync(default)).ReturnsAsync(expectedImposters);

			var result = await Client.GetImpostersAsync();

			Assert.Same(expectedImposters, result);
		}
	}
}

