using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Imposters
{
    /// <summary>
    /// Summary description for ImposterTests
    /// </summary>
    [TestClass]
    public class HttpsImposterTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_SetsPort()
        {
            const int port = 123;
            var imposter = new HttpsImposter(port, null);
            Assert.AreEqual(port, imposter.Port);
        }

        [TestMethod]
        public void Constructor_SetsProtocol()
        {
            var imposter = new HttpsImposter(123, null);
            Assert.AreEqual("https", imposter.Protocol);
        }

        [TestMethod]
        public void Constructor_SetsName()
        {
            const string expectedName = "Service";
            var imposter = new HttpsImposter(123, expectedName);
            Assert.AreEqual(expectedName, imposter.Name);
        }

        [TestMethod]
        public void Constructor_AllowsNullPort()
        {
            var imposter = new HttpsImposter(null, null);
            Assert.AreEqual(default(int), imposter.Port);
        }

        [TestMethod]
        public void Constructor_InitializesStubsCollection()
        {
            var imposter = new HttpsImposter(123, null);
            Assert.IsNotNull(imposter.Stubs);
        }

        [TestMethod]
        public void Constructor_SetsKey()
        {
            var expectedKeyValue = "testKey";
            var imposter = new HttpsImposter(123, null, expectedKeyValue, null, false);
            Assert.AreEqual(expectedKeyValue, imposter.Key);
        }

        [TestMethod]
        public void Constructor_SetsKeyAsNullWhenMissing()
        {
            var imposter = new HttpsImposter(123, null);
            Assert.IsNull(imposter.Key);
        }

        [TestMethod]
        public void Constructor_SetsCert()
        {
            var expectedCertValue = "testCert";
            var imposter = new HttpsImposter(123, null, null, expectedCertValue, false);
            Assert.AreEqual(expectedCertValue, imposter.Cert);
        }

        [TestMethod]
        public void Constructor_SetsCertAsNullWhenMissing()
        {
            var imposter = new HttpsImposter(123, null);
            Assert.IsNull(imposter.Cert);
        }

        [TestMethod]
        public void Constructor_SetsMutualAuth()
        {
            var imposter = new HttpsImposter(123, null, null, null, true);
            Assert.IsTrue(imposter.MutualAuthRequired);
        }

        [TestMethod]
        public void Constructor_SetsMutualAuthFalseWhenMissing()
        {
            var imposter = new HttpsImposter(123, null);
            Assert.IsFalse(imposter.MutualAuthRequired);
        }

        #endregion

        #region Stub Tests

        [TestMethod]
        public void AddStub_AddsStubToCollection()
        {
            var imposter = new HttpsImposter(123, null);
            imposter.AddStub();
            Assert.AreEqual(1, imposter.Stubs.Count);
        }
                
        #endregion
    }
}
