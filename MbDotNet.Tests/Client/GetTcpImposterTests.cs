using System.Threading.Tasks;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Moq;
using Xunit;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class GetTcpImposterTests : MountebankClientTestBase
	{
		[Fact]
		public async Task NonTcpImposterRetrieved_ThrowsInvalidProtocolException()
		{
			const int port = 6000;
			MockRequestProxy.Setup(x => x.GetTcpImposterAsync(port, default)).ReturnsAsync(new RetrievedTcpImposter { Protocol = "Http" });

			var exception = await Assert.ThrowsAsync<InvalidProtocolException>(async () =>
			{
				await Client.GetTcpImposterAsync(port);
			});
		}

		[Fact]
		public async Task TcpImposterRetrieved_ReturnsImposter()
		{
			const int port = 6000;
			var expectedImposter = new RetrievedTcpImposter
			{
				Port = port,
				Protocol = "Tcp"
			};

			MockRequestProxy.Setup(x => x.GetTcpImposterAsync(port, default)).ReturnsAsync(expectedImposter);

			var result = await Client.GetTcpImposterAsync(port);

			Assert.Same(expectedImposter, result);
		}
	}
}
