using System.Threading.Tasks;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Moq;
using Xunit;

using MSAssert = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class GetHttpsImposterTests : MountebankClientTestBase
	{
		[Fact]
		public async Task NonHttpsImposterRetrieved_ThrowsInvalidProtocolException()
		{
			const int port = 6000;
			MockRequestProxy.Setup(x => x.GetHttpsImposterAsync(port, default)).ReturnsAsync(new RetrievedHttpsImposter() { Protocol = "Tcp" });

			await Assert.ThrowsAsync<InvalidProtocolException>(async () =>
			{
				await Client.GetHttpsImposterAsync(port);
			});
		}

		[Fact]
		public async Task HttpsImposterRetrieved_ReturnsImposter()
		{
			const int port = 6000;
			var expectedImposter = new RetrievedHttpsImposter
			{
				Port = port,
				Protocol = "Https"
			};

			MockRequestProxy.Setup(x => x.GetHttpsImposterAsync(port, default)).ReturnsAsync(expectedImposter);

			var result = await Client.GetHttpsImposterAsync(port);

			Assert.Same(expectedImposter, result);
		}
	}
}
