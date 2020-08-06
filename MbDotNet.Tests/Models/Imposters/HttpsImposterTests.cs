using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Imposters
{
    /// <summary>
    /// Summary description for ImposterTests
    /// </summary>
    [TestClass, TestCategory("Unit")]
    public class HttpsImposterTests
    {
        #region Constructor Tests

        [TestMethod]
        public void HttpsImposter_Constructor_SetsPort()
        {
            const int port = 123;
            var imposter = new HttpsImposter(port, null);
            Assert.AreEqual(port, imposter.Port);
        }

        [TestMethod]
        public void HttpsImposter_Constructor_SetsProtocol()
        {
            var imposter = new HttpsImposter(123, null);
            Assert.AreEqual("https", imposter.Protocol);
        }

        [TestMethod]
        public void HttpsImposter_Constructor_SetsName()
        {
            const string expectedName = "Service";
            var imposter = new HttpsImposter(123, expectedName);
            Assert.AreEqual(expectedName, imposter.Name);
        }

        [TestMethod]
        public void HttpsImposter_Constructor_AllowsNullPort()
        {
            var imposter = new HttpsImposter(null, null);
            Assert.AreEqual(default(int), imposter.Port);
        }

        [TestMethod]
        public void HttpsImposter_Constructor_InitializesStubsCollection()
        {
            var imposter = new HttpsImposter(123, null);
            Assert.IsNotNull(imposter.Stubs);
        }

        [TestMethod]
        public void HttpsImposter_Constructor_SetsKey()
        {
            var expectedKeyValue = "testKey";
            var imposter = new HttpsImposter(123, null, expectedKeyValue, null, false);
            Assert.AreEqual(expectedKeyValue, imposter.Key);
        }

        [TestMethod]
        public void HttpsImposter_Constructor_SetsKeyAsNullWhenMissing()
        {
            var imposter = new HttpsImposter(123, null);
            Assert.IsNull(imposter.Key);
        }

        [TestMethod]
        public void HttpsImposter_Constructor_SetsCert()
        {
            var expectedCertValue = "testCert";
            var imposter = new HttpsImposter(123, null, null, expectedCertValue, false);
            Assert.AreEqual(expectedCertValue, imposter.Cert);
        }

        [TestMethod]
        public void HttpsImposter_Constructor_SetsCertAsNullWhenMissing()
        {
            var imposter = new HttpsImposter(123, null);
            Assert.IsNull(imposter.Cert);
        }

        [TestMethod]
        public void HttpsImposter_Constructor_SetsMutualAuth()
        {
            var imposter = new HttpsImposter(123, null, null, null, true);
            Assert.IsTrue(imposter.MutualAuthRequired);
        }

        [TestMethod]
        public void HttpsImposter_Constructor_SetsMutualAuthFalseWhenMissing()
        {
            var imposter = new HttpsImposter(123, null);
            Assert.IsFalse(imposter.MutualAuthRequired);
        }

        [TestMethod]
        public void HttpsImposter_Constructor_SetsDefaultResponse()
        {
            var imposter = new HttpsImposter(123, null, defaultResponse: new HttpResponseFields());
            Assert.IsNotNull(imposter.DefaultResponse);
        }

        #endregion

        #region Stub Tests

        [TestMethod]
        public void HttpsImposter_AddStub_AddsStubToCollection()
        {
            var imposter = new HttpsImposter(123, null);
            imposter.AddStub();
            Assert.AreEqual(1, imposter.Stubs.Count);
        }
                
        #endregion
    }
}
