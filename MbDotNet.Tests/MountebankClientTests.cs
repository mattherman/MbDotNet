using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MbDotNet.Enums;
using MbDotNet.Interfaces;
using MbDotNet.Models;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Imposters;
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
        public void CreateHttpImposter_AddsNewImposterToCollection()
        {
            _client.CreateHttpImposter(123);
            Assert.AreEqual(1, _client.Imposters.Count);
        }

        [TestMethod]
        public void Submit_CallsSubmitOnAllPendingImposters()
        {
            const int firstPortNumber = 123;
            const int secondPortNumber = 456;

            _client.Imposters.Add(new HttpImposter(firstPortNumber, null));
            _client.Imposters.Add(new HttpImposter(secondPortNumber, null));

            _mockRequestProxy.Setup(x => x.CreateImposter(It.Is<Imposter>(imp => imp.Port == firstPortNumber)));
            _mockRequestProxy.Setup(x => x.CreateImposter(It.Is<Imposter>(imp => imp.Port == secondPortNumber)));

            _client.Submit();

            _mockRequestProxy.Verify(x => x.CreateImposter(It.Is<Imposter>(imp => imp.Port == firstPortNumber)), Times.Once);
            _mockRequestProxy.Verify(x => x.CreateImposter(It.Is<Imposter>(imp => imp.Port == secondPortNumber)), Times.Once);
        }

        [TestMethod]
        public void Submit_SetsPendingSubmissionFalse()
        {
            _client.Imposters.Add(new HttpImposter(8080, null));

            _client.Submit();

            Assert.IsFalse(_client.Imposters.First().PendingSubmission);
        }

        [TestMethod]
        public void Submit_DoesNotSubmitNonPendingImposters()
        {
            var mockImposter = new Mock<HttpImposter>(123, null);
            mockImposter.SetupGet(x => x.PendingSubmission).Returns(false);

            _client.Imposters.Add(mockImposter.Object);

            _client.Submit();

            _mockRequestProxy.Verify(x => x.CreateImposter(It.IsAny<HttpImposter>()), Times.Never);
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
