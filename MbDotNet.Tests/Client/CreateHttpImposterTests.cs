using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class CreateHttpImposterTests : MountebankClientTestBase
    {
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