using System.Threading.Tasks;
using Moq;
using Xunit;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class DeleteImposterTests : MountebankClientTestBase
	{
		[Fact]
		public async Task CallsDelete()
		{
			const int port = 8080;

			MockRequestProxy.Setup(x => x.DeleteImposterAsync(port, default)).Returns(Task.CompletedTask);

			await Client.DeleteImposterAsync(port);

			MockRequestProxy.Verify(x => x.DeleteImposterAsync(port, default), Times.Once);
		}

		[Fact]
		public async Task DeleteAllImposters_CallsDeleteAll()
		{
			MockRequestProxy.Setup(x => x.DeleteAllImpostersAsync(default)).Returns(Task.CompletedTask);

			await Client.DeleteAllImpostersAsync();

			MockRequestProxy.Verify(x => x.DeleteAllImpostersAsync(default), Times.Once);
		}
	}
}
