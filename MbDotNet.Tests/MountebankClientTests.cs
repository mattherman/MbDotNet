using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MbDotNet.Enums;
using MbDotNet.Interfaces;
using MbDotNet.Models;
using Moq;

namespace MbDotNet.Tests
{
    [TestClass]
    public class MountebankClientTests
    {
        private IClient _client;
        private Mock<IRequestProxy> _mockRequestProxy;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRequestProxy = new Mock<IRequestProxy>();
            _client = new MountebankClient(_mockRequestProxy.Object);
        }

        [TestMethod]
        public void Constructor_InitializesImposterCollection()
        {
            var client = new MountebankClient();
            Assert.IsNotNull(client.Imposters);
        }

        [TestMethod]
        public void Create_AddsNewImposterToCollection()
        {
            _client.CreateImposter(123, Protocol.Http);
            Assert.AreEqual(1, _client.Imposters.Count);
        }

        [TestMethod]
        public void Submit_CallsSubmitOnAllPendingImposters()
        {
            const int firstPortNumber = 123;
            const int secondPortNumber = 456;

            _client.Imposters.Add(new Imposter(firstPortNumber, Protocol.Http));
            _client.Imposters.Add(new Imposter(secondPortNumber, Protocol.Http));

            _mockRequestProxy.Setup(x => x.CreateImposter(It.Is<IImposter>(imp => imp.Port == firstPortNumber)));
            _mockRequestProxy.Setup(x => x.CreateImposter(It.Is<IImposter>(imp => imp.Port == secondPortNumber)));

            _client.Submit();

            _mockRequestProxy.Verify(x => x.CreateImposter(It.Is<IImposter>(imp => imp.Port == firstPortNumber)), Times.Once);
            _mockRequestProxy.Verify(x => x.CreateImposter(It.Is<IImposter>(imp => imp.Port == secondPortNumber)), Times.Once);
        }

        [TestMethod]
        public void Submit_SetsPendingSubmissionFalse()
        {
            _client.Imposters.Add(new Imposter(8080, Protocol.Http));

            _client.Submit();

            Assert.IsFalse(_client.Imposters.First().PendingSubmission);
        }

        [TestMethod]
        public void Submit_DoesNotSubmitNonPendingImposters()
        {
            var mockImposter = new Mock<IImposter>();
            mockImposter.SetupGet(x => x.PendingSubmission).Returns(false);

            _client.Imposters.Add(mockImposter.Object);

            _client.Submit();

            _mockRequestProxy.Verify(x => x.CreateImposter(It.IsAny<IImposter>()), Times.Never);
        }

        [TestMethod]
        public void DeleteImposter_CallsDelete()
        {
            const int port = 8080;

            _client.Imposters.Add(new Imposter(port, Protocol.Http));

            _mockRequestProxy.Setup(x => x.DeleteImposter(port));

            _client.DeleteImposter(port);

            _mockRequestProxy.Verify(x => x.DeleteImposter(port), Times.Once);
        }

        [TestMethod]
        public void DeleteImposter_RemovesImposterFromCollection()
        {
            const int port = 8080;

            _client.Imposters.Add(new Imposter(port, Protocol.Http));

            _client.DeleteImposter(port);

            Assert.AreEqual(0, _client.Imposters.Count);
        }

        [TestMethod]
        public void DeleteAllImposters_CallsDeleteAll()
        {
            _mockRequestProxy.Setup(x => x.DeleteAllImposters());

            _client.Imposters.Add(new Imposter(123, Protocol.Http));
            _client.Imposters.Add(new Imposter(456, Protocol.Tcp));

            _client.DeleteAllImposters();

            _mockRequestProxy.Verify(x => x.DeleteAllImposters(), Times.Once);
        }

        [TestMethod]
        public void DeleteAllImposters_RemovesAllImpostersFromCollection()
        {
            _mockRequestProxy.Setup(x => x.DeleteAllImposters());

            _client.Imposters.Add(new Imposter(123, Protocol.Http));
            _client.Imposters.Add(new Imposter(456, Protocol.Tcp));

            _client.DeleteAllImposters();

            Assert.AreEqual(0, _client.Imposters.Count);
        }

        [TestMethod]
        public void Test()
        {
            var client = new MountebankClient();

            client.DeleteAllImposters();

            var responseObject = new {Name = "Ten", Value = 10};
            client.CreateImposter(5738, Protocol.Http).AddStub().ReturnsJson(HttpStatusCode.OK, responseObject);

            client.Submit();
        }
    }
}
