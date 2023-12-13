using System.Threading.Tasks;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Moq;
using Xunit;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class GetHttpImposterTests : MountebankClientTestBase
	{
		[Fact]
		public async Task NonHttpImposterRetrieved_ThrowsInvalidProtocolException()
		{
			const int port = 6000;
			MockRequestProxy.Setup(x => x.GetHttpImposterAsync(port, default)).ReturnsAsync(new RetrievedHttpImposter { Protocol = "Tcp" });

			await Assert.ThrowsAsync<InvalidProtocolException>(async () =>
			{
				await Client.GetHttpImposterAsync(port);
			});
		}

		[Fact]
		public async Task HttpImposterRetrieved_ReturnsImposter()
		{
			const int port = 6000;
			var expectedImposter = new RetrievedHttpImposter
			{
				Port = port,
				Protocol = "Http"
			};

			MockRequestProxy.Setup(x => x.GetHttpImposterAsync(port, default)).ReturnsAsync(expectedImposter);

			var result = await Client.GetHttpImposterAsync(port);

			Assert.Same(expectedImposter, result);
		}
	}
}
