using System.Linq;

using MbDotNet.Interfaces;
using MbDotNet.Models.Imposters;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class MountebankClientTests
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
        public void Constructor_InitializesImposterCollection()
        {
            var client = new MountebankClient();
            Assert.IsNotNull(client.Imposters);
        }

        [TestMethod]
        public void CreateTcpImposter_ShouldNotAddNewImposterToCollection()
        {
            this._client.CreateTcpImposter(123);
            Assert.AreEqual(0, this._client.Imposters.Count);
        }

        [TestMethod]
        public void Submit_CallsSubmitOnAllPendingImposters()
        {
            const int firstPortNumber = 123;
            const int secondPortNumber = 456;

            var imposter1 = new HttpImposter(firstPortNumber, null);
            var imposter2 = new HttpImposter(secondPortNumber, null);
          
            this._client.Submit(new [] { imposter1, imposter2});

            this._mockRequestProxy.Verify(x => x.CreateImposter(It.Is<Imposter>(imp => imp.Port == firstPortNumber)), Times.Once);
            this._mockRequestProxy.Verify(x => x.CreateImposter(It.Is<Imposter>(imp => imp.Port == secondPortNumber)), Times.Once);
        }

        [TestMethod]
        public void DeleteImposter_CallsDelete()
        {
            const int port = 8080;

            this._client.Imposters.Add(new HttpImposter(port, null));

            this._mockRequestProxy.Setup(x => x.DeleteImposter(port));

            this._client.DeleteImposter(port);

            this._mockRequestProxy.Verify(x => x.DeleteImposter(port), Times.Once);
        }

        [TestMethod]
        public void DeleteImposter_RemovesImposterFromCollection()
        {
            const int port = 8080;

            this._client.Imposters.Add(new HttpImposter(port, null));

            this._client.DeleteImposter(port);

            Assert.AreEqual(0, this._client.Imposters.Count);
        }

        [TestMethod]
        public void DeleteAllImposters_CallsDeleteAll()
        {
            this._mockRequestProxy.Setup(x => x.DeleteAllImposters());

            this._client.Imposters.Add(new HttpImposter(123, null));
            this._client.Imposters.Add(new HttpImposter(456, null));

            this._client.DeleteAllImposters();

            this._mockRequestProxy.Verify(x => x.DeleteAllImposters(), Times.Once);
        }

        [TestMethod]
        public void DeleteAllImposters_RemovesAllImpostersFromCollection()
        {
            this._mockRequestProxy.Setup(x => x.DeleteAllImposters());

            this._client.Imposters.Add(new HttpImposter(123, null));
            this._client.Imposters.Add(new HttpImposter(456, null));

            this._client.DeleteAllImposters();

            Assert.AreEqual(0, this._client.Imposters.Count);
        }
         
        [TestMethod]
        public void SubmitCollection_ShouldSubmitImpostersUsingProxy()
        {
            const int firstPortNumber = 123;
            const int secondPortNumber = 456;

            var imposter1 = this._client.CreateHttpImposter(firstPortNumber);
            var imposter2 = this._client.CreateHttpImposter(secondPortNumber);
            
            this._client.Submit(new[] { imposter1, imposter2 });

            this._mockRequestProxy.Verify(x => x.CreateImposter(It.Is<Imposter>(imp => imp.Port == firstPortNumber)), Times.Once);
            this._mockRequestProxy.Verify(x => x.CreateImposter(It.Is<Imposter>(imp => imp.Port == secondPortNumber)), Times.Once);
        }

        [TestMethod]
        public void SubmitCollection_ShouldAddImpostersToCollection()
        {
            const int firstPortNumber = 123;
            const int secondPortNumber = 456;

            var imposter1 = this._client.CreateHttpImposter(firstPortNumber);
            var imposter2 = this._client.CreateHttpImposter(secondPortNumber);

            this._client.Submit(new[] { imposter1, imposter2 });

            Assert.AreEqual(1, this._client.Imposters.Count(x => x.Port == firstPortNumber));
            Assert.AreEqual(1, this._client.Imposters.Count(x => x.Port == secondPortNumber));
        }
    }
}
