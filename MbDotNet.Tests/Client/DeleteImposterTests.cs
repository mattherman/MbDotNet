using MbDotNet.Interfaces;
using MbDotNet.Models.Imposters;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class DeleteImposterTests
    {
        private IClient _client;
        private Mock<IRequestProxy> _mockRequestProxy;

        [TestInitialize]
        public void TestInitialize()
        {
            this._mockRequestProxy = new Mock<IRequestProxy>();
            this._client = new MountebankClient(this._mockRequestProxy.Object);
        }

        [TestMethod]
        public void DeleteImposter_CallsDelete()
        {
            const int port = 8080;

            _client.Imposters.Add(new HttpImposter(port, null));

            _mockRequestProxy.Setup(x => x.DeleteImposter(port));

            _client.DeleteImposter(port);

            _mockRequestProxy.Verify(x => x.DeleteImposter(port), Times.Once);
        }

        [TestMethod]
        public void DeleteImposter_RemovesImposterFromCollection()
        {
            const int port = 8080;

            _client.Imposters.Add(new HttpImposter(port, null));

            _client.DeleteImposter(port);

            Assert.AreEqual(0, _client.Imposters.Count);
        }

        [TestMethod]
        public void DeleteAllImposters_CallsDeleteAll()
        {
            _mockRequestProxy.Setup(x => x.DeleteAllImposters());

            _client.Imposters.Add(new HttpImposter(123, null));
            _client.Imposters.Add(new HttpImposter(456, null));

            _client.DeleteAllImposters();

            _mockRequestProxy.Verify(x => x.DeleteAllImposters(), Times.Once);
        }

        [TestMethod]
        public void DeleteAllImposters_RemovesAllImpostersFromCollection()
        {
            _mockRequestProxy.Setup(x => x.DeleteAllImposters());

            _client.Imposters.Add(new HttpImposter(123, null));
            _client.Imposters.Add(new HttpImposter(456, null));

            _client.DeleteAllImposters();

            Assert.AreEqual(0, _client.Imposters.Count);
        }
    }
}