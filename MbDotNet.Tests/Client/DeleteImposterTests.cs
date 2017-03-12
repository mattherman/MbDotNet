using MbDotNet.Models.Imposters;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class DeleteImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        public void CallsDelete()
        {
            const int port = 8080;

            Client.Imposters.Add(new HttpImposter(port, null));

            MockRequestProxy.Setup(x => x.DeleteImposter(port));

            Client.DeleteImposter(port);

            MockRequestProxy.Verify(x => x.DeleteImposter(port), Times.Once);
        }

        [TestMethod]
        public void RemovesImposterFromCollection()
        {
            const int port = 8080;

            Client.Imposters.Add(new HttpImposter(port, null));

            Client.DeleteImposter(port);

            Assert.AreEqual(0, Client.Imposters.Count);
        }

        [TestMethod]
        public void DeleteAllImposters_CallsDeleteAll()
        {
            MockRequestProxy.Setup(x => x.DeleteAllImposters());

            Client.Imposters.Add(new HttpImposter(123, null));
            Client.Imposters.Add(new HttpImposter(456, null));

            Client.DeleteAllImposters();

            MockRequestProxy.Verify(x => x.DeleteAllImposters(), Times.Once);
        }

        [TestMethod]
        public void DeleteAllImposters_RemovesAllImpostersFromCollection()
        {
            MockRequestProxy.Setup(x => x.DeleteAllImposters());

            Client.Imposters.Add(new HttpImposter(123, null));
            Client.Imposters.Add(new HttpImposter(456, null));

            Client.DeleteAllImposters();

            Assert.AreEqual(0, Client.Imposters.Count);
        }
    }
}