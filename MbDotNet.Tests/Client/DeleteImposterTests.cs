using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MbDotNet.Tests.Client
{
	[TestClass, TestCategory("Unit")]
	public class DeleteImposterTests : MountebankClientTestBase
	{
		[TestMethod]
		public async Task CallsDelete()
		{
			const int port = 8080;

			MockRequestProxy.Setup(x => x.DeleteImposterAsync(port, default)).Returns(Task.CompletedTask);

			await Client.DeleteImposterAsync(port).ConfigureAwait(false);

			MockRequestProxy.Verify(x => x.DeleteImposterAsync(port, default), Times.Once);
		}

		[TestMethod]
		public async Task DeleteAllImposters_CallsDeleteAll()
		{
			MockRequestProxy.Setup(x => x.DeleteAllImpostersAsync(default)).Returns(Task.CompletedTask);

			await Client.DeleteAllImpostersAsync().ConfigureAwait(false);

			MockRequestProxy.Verify(x => x.DeleteAllImpostersAsync(default), Times.Once);
		}
	}
}
