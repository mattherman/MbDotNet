using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class CreateHttpsImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        public void CreateHttpsImposter_ShouldNotAddNewImposterToCollection()
        {
            _client.CreateHttpsImposter(123);
            Assert.AreEqual(0, _client.Imposters.Count);
        }
        
        [TestMethod]
        public void CreateHttpsImposter_WithoutName_SetsNameToNull()
        {
            var imposter = _client.CreateHttpsImposter(123);
            
            Assert.IsNotNull(imposter);
            Assert.IsNull(imposter.Name);
        }

        [TestMethod]
        public void CreateHttpsImposter_WithName_SetsName()
        {
            const string expectedName = "Service";

            var imposter = _client.CreateHttpsImposter(123, expectedName);
            
            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedName, imposter.Name);
        }

        [TestMethod]
        public void CreateHttpsImposter_WithKey_SetsKey()
        {
            const string expectedKey = "key";

            var imposter = _client.CreateHttpsImposter(123, null, expectedKey);

            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedKey, imposter.Key);
        }

        [TestMethod]
        public void CreateHttpsImposter_WithCert_SetsCert()
        {
            const string expectedCert = "cert";

            var imposter = _client.CreateHttpsImposter(123, null, null, expectedCert);

            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedCert, imposter.Cert);
        }
    }
}