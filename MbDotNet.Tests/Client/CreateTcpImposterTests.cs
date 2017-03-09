using MbDotNet.Enums;
using MbDotNet.Interfaces;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class CreateTcpImposterTests
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
        public void CreateTcpImposter_WithoutName_SetsNameToNull()
        {
            var imposter = _client.CreateTcpImposter(123);

            Assert.IsNotNull(imposter);
            Assert.IsNull(imposter.Name);
        }

        [TestMethod]
        public void CreateTcpImposter_WithName_SetsName()
        {
            const string expectedName = "Service";

            var imposter = _client.CreateTcpImposter(123, expectedName);
            
            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedName, imposter.Name);
        }

        [TestMethod]
        public void CreateTcpImposter_WithoutMode_SetsModeToText()
        {
            const string expectedMode = "text";

            var imposter = _client.CreateTcpImposter(123, null);
            
            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedMode, imposter.Mode);
        }

        [TestMethod]
        public void CreateTcpImposter_WithMode_SetsMode()
        {
            const string expectedMode = "binary";

            var imposter = _client.CreateTcpImposter(123, null, TcpMode.Binary);

            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedMode, imposter.Mode);
        }
    }
}