using MbDotNet.Models.Imposters;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Imposters
{
    /// <summary>
    /// Summary description for ImposterTests
    /// </summary>
    [TestClass, TestCategory("Unit")]
    public class HttpImposterTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_SetsPort()
        {
            const int port = 123;
            var imposter = new HttpImposter(port, null);
            Assert.AreEqual(port, imposter.Port);
        }

        [TestMethod]
        public void Constructor_SetsProtocol()
        {
            var imposter = new HttpImposter(123, null);
            Assert.AreEqual("http", imposter.Protocol);
        }

        [TestMethod]
        public void Constructor_SetsName()
        {
            const string expectedName = "Service";
            var imposter = new HttpImposter(123, expectedName);
            Assert.AreEqual(expectedName, imposter.Name);
        }

        [TestMethod]
        public void Constructor_AllowsNullPort()
        {
            var imposter = new HttpImposter(null, null);
            Assert.AreEqual(default(int), imposter.Port);
        }

        [TestMethod]
        public void Constructor_InitializesStubsCollection()
        {
            var imposter = new HttpImposter(123, null);
            Assert.IsNotNull(imposter.Stubs);
        }
        
        [TestMethod]
        public void Constructor_InitializesDefaultResponse()
        {
            var imposter = new HttpImposter(123, null, defaultResponse: new HttpResponseFields());
            Assert.IsNotNull(imposter.DefaultResponse);
        }

        #endregion

        #region Stub Tests

        [TestMethod]
        public void AddStub_AddsStubToCollection()
        {
            var imposter = new HttpImposter(123, null);
            imposter.AddStub();
            Assert.AreEqual(1, imposter.Stubs.Count);
        }
                
        #endregion
    }
}
