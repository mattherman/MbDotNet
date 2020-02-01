using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace MbDotNet.Tests.Client
{
    [TestClass, TestCategory("Unit")]
    public class DeleteImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        public async Task CallsDelete()
        {
            const int port = 8080;

            Client.Imposters.Add(new HttpImposter(port, null));

            MockRequestProxy.Setup(x => x.DeleteImposterAsync(port, default)).Returns(Task.CompletedTask);

            await Client.DeleteImposterAsync(port).ConfigureAwait(false);

            MockRequestProxy.Verify(x => x.DeleteImposterAsync(port, default), Times.Once);
        }

        [TestMethod]
        public async Task RemovesImposterFromCollection()
        {
            const int port = 8080;

            Client.Imposters.Add(new HttpImposter(port, null));

            await Client.DeleteImposterAsync(port).ConfigureAwait(false);

            Assert.AreEqual(0, Client.Imposters.Count);
        }

        [TestMethod]
        public async Task DeleteAllImposters_CallsDeleteAll()
        {
            MockRequestProxy.Setup(x => x.DeleteAllImpostersAsync(default)).Returns(Task.CompletedTask);

            Client.Imposters.Add(new HttpImposter(123, null));
            Client.Imposters.Add(new HttpImposter(456, null));

            await Client.DeleteAllImpostersAsync().ConfigureAwait(false);

            MockRequestProxy.Verify(x => x.DeleteAllImpostersAsync(default), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAllImposters_RemovesAllImpostersFromCollection()
        {
            MockRequestProxy.Setup(x => x.DeleteAllImpostersAsync(default)).Returns(Task.CompletedTask);

            Client.Imposters.Add(new HttpImposter(123, null));
            Client.Imposters.Add(new HttpImposter(456, null));

            await Client.DeleteAllImpostersAsync().ConfigureAwait(false);

            Assert.AreEqual(0, Client.Imposters.Count);
        }
    }
}