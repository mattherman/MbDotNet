using MbDotNet.Interfaces;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class CreateHttpImposterTests
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
        public void CreateHttpImposter_ShouldNotAddNewImposterToCollection()
        {
            _client.CreateHttpImposter(123);
            Assert.AreEqual(0, _client.Imposters.Count);
        }
        
        [TestMethod]
        public void CreateHttpImposter_WithoutName_SetsNameToNull()
        {
            var imposter = _client.CreateHttpImposter(123);
            
            Assert.IsNotNull(imposter);
            Assert.IsNull(imposter.Name);
        }

        [TestMethod]
        public void CreateHttpImposter_WithName_SetsName()
        {
            const string expectedName = "Service";

            var imposter = _client.CreateHttpImposter(123, expectedName);
            
            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedName, imposter.Name);
        }
    }
}