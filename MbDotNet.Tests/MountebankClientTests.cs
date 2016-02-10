using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MbDotNet.Enums;
using MbDotNet.Interfaces;
using Moq;

namespace MbDotNet.Tests
{
    [TestClass]
    public class MountebankClientTests
    {
        private IClient _client;

        [TestInitialize]
        public void TestInitialize()
        {
            _client = new MountebankClient();
        }

        [TestMethod]
        public void Create_AddsNewImposterToCollection()
        {
            _client.CreateImposter(123, Protocol.Http);
            Assert.AreEqual(1, _client.Imposters.Count);
        }

        [TestMethod]
        public void SubmitAll_SubmitsAllPendingImposters()
        {
            var firstMockPendingImposter = new Mock<IImposter>();
            firstMockPendingImposter.SetupGet(x => x.PendingSubmission).Returns(true);
            firstMockPendingImposter.Setup(x => x.Submit());

            var secondMockPendingImposter = new Mock<IImposter>();
            secondMockPendingImposter.SetupGet(x => x.PendingSubmission).Returns(true);
            secondMockPendingImposter.Setup(x => x.Submit());

            _client.Imposters.Add(firstMockPendingImposter.Object);
            _client.Imposters.Add(secondMockPendingImposter.Object);

            _client.SubmitAll();

            firstMockPendingImposter.Verify(x => x.Submit());
            secondMockPendingImposter.Verify(x => x.Submit());
        }

        [TestMethod]
        public void SubmitAll_DoesNotSubmitNonPendingImposters()
        {
            var mockImposter = new Mock<IImposter>();
            mockImposter.SetupGet(x => x.PendingSubmission).Returns(false);

            _client.Imposters.Add(mockImposter.Object);

            _client.SubmitAll();

            mockImposter.Verify(x => x.Submit(), Times.Never);
        }

        [TestMethod]
        public void DeleteImposter_CallsDelete()
        {
            const int port = 8080;

            var mockImposter = new Mock<IImposter>();
            mockImposter.SetupGet(x => x.Port).Returns(port);
            mockImposter.Setup(x => x.Delete());

            _client.Imposters.Add(mockImposter.Object);

            _client.DeleteImposter(port);

            mockImposter.Verify(x => x.Delete(), Times.Once);
        }

        [TestMethod]
        public void DeleteImposter_RemovesImposterFromCollection()
        {
            const int port = 8080;

            var mockImposter = new Mock<IImposter>();
            mockImposter.SetupGet(x => x.Port).Returns(port);
            mockImposter.Setup(x => x.Delete());

            _client.Imposters.Add(mockImposter.Object);

            _client.DeleteImposter(port);

            Assert.AreEqual(0, _client.Imposters.Count);
        }

        [TestMethod]
        public void DeleteAllImposters_CallsDeleteOnAllImposters()
        {
            var firstMockPendingImposter = new Mock<IImposter>();
            firstMockPendingImposter.Setup(x => x.Delete());

            var secondMockPendingImposter = new Mock<IImposter>();
            secondMockPendingImposter.Setup(x => x.Delete());

            _client.Imposters.Add(firstMockPendingImposter.Object);
            _client.Imposters.Add(secondMockPendingImposter.Object);

            _client.DeleteAllImposters();

            firstMockPendingImposter.Verify(x => x.Delete(), Times.Once);
            secondMockPendingImposter.Verify(x => x.Delete(), Times.Once);
        }

        [TestMethod]
        public void DeleteAllImposters_RemovesAllImpostersFromCollection()
        {
            var firstMockPendingImposter = new Mock<IImposter>();
            firstMockPendingImposter.Setup(x => x.Delete());

            var secondMockPendingImposter = new Mock<IImposter>();
            secondMockPendingImposter.Setup(x => x.Delete());

            _client.Imposters.Add(firstMockPendingImposter.Object);
            _client.Imposters.Add(secondMockPendingImposter.Object);

            _client.DeleteAllImposters();

            Assert.AreEqual(0, _client.Imposters.Count);
        }

        [TestMethod]
        public void Test()
        {
            _client.DeleteAllImposters();
            var testObj = new {Status = "Available", Name = "Test"};
            _client.CreateImposter(5738, Protocol.Http).Returns(HttpStatusCode.OK, testObj).Submit();
        }
    }
}
