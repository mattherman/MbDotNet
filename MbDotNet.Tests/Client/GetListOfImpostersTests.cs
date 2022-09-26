using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MbDotNet.Tests.Client
{
	[TestClass, TestCategory("Unit")]
	public class GetListOfImpostersTests : MountebankClientTestBase
	{
		[TestMethod]
		public async Task ImpostersRetrieved_ReturnsImposters()
		{
			await Client.DeleteAllImpostersAsync();

			var expectedImposters = new List<SimpleRetrievedImposter>
			{
				new SimpleRetrievedImposter { Port=123, Protocol = "Http" },
				new SimpleRetrievedImposter { Port=456, Protocol = "Http" }
			};
			MockRequestProxy.Setup(x => x.GetImpostersAsync(default)).ReturnsAsync(expectedImposters);

			var result = await Client.GetImpostersAsync().ConfigureAwait(false);

			Assert.AreSame(expectedImposters, result);
		}
	}
}

