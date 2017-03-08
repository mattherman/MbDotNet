using Microsoft.VisualStudio.TestTools.UnitTesting;
using MbDotNet.Enums;
using MbDotNet.Models;
using MbDotNet.Models.Imposters;

namespace MbDotNet.Tests.Imposters
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
        public void Constructor_InitializesStubsCollection()
        {
            var imposter = new HttpsImposter(123, null);
            Assert.IsNotNull(imposter.Stubs);
        }

        [TestMethod]
        public void Constructor_SetsKey()
        {
            var testKeyValue = "testKey";
            var imposter = new HttpsImposter(123, null, testKeyValue);
            Assert.AreEqual(imposter.Key, testKeyValue);
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
