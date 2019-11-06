using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
    [TestClass, TestCategory("Unit")]
    public class CreateHttpsImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        public void HttpsImposter_ShouldNotAddNewImposterToCollection()
        {
            Client.CreateHttpsImposter(123);
            Assert.AreEqual(0, Client.Imposters.Count);
        }
        
        [TestMethod]
        public void HttpsImposter_WithoutName_SetsNameToNull()
        {
            var imposter = Client.CreateHttpsImposter(123);
            
            Assert.IsNotNull(imposter);
            Assert.IsNull(imposter.Name);
        }

        [TestMethod]
        public void HttpsImposter_WithName_SetsName()
        {
            const string expectedName = "Service";

            var imposter = Client.CreateHttpsImposter(123, expectedName);
            
            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedName, imposter.Name);
        }

        [TestMethod]
        public void HttpsImposter_WithKey_SetsKey()
        {
            const string expectedKey = "key";

            var imposter = Client.CreateHttpsImposter(123, null, expectedKey);

            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedKey, imposter.Key);
        }

        [TestMethod]
        public void HttpsImposter_WithCert_SetsCert()
        {
            const string expectedCert = "cert";

            var imposter = Client.CreateHttpsImposter(123, null, null, expectedCert);

            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedCert, imposter.Cert);
        }

        [TestMethod]
        public void HttpsImposter_WithoutPortAndName_SetsPortAndNameToNull()
        {
            var imposter = Client.CreateHttpsImposter();

            Assert.IsNotNull(imposter);
            Assert.AreEqual(default(int), imposter.Port);
            Assert.IsNull(imposter.Name);
        }

        [TestMethod]
        public void HttpsImposter_WithoutRecordRequests_SetsRecordRequest()
        {
            var imposter = Client.CreateHttpsImposter();

            Assert.IsFalse(imposter.RecordRequests);
        }

        [TestMethod]
        public void HttpsImposter_WithRecordRequests_SetsRecordRequest()
        {
            const bool recordRequests = true;

            var imposter = Client.CreateHttpsImposter(recordRequests: recordRequests);

            Assert.IsTrue(imposter.RecordRequests);
        }
    }
}