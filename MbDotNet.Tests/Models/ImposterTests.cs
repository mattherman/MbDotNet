using Microsoft.VisualStudio.TestTools.UnitTesting;
using MbDotNet.Enums;

namespace MbDotNet.Tests
{
    /// <summary>
    /// Summary description for ImposterTests
    /// </summary>
    [TestClass]
    public class ImposterTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Constructor_SetsPort()
        {
            const int port = 123;
            var imposter = new Imposter(port, Protocol.Http);
            Assert.AreEqual(port, imposter.Port);
        }

        [TestMethod]
        public void Constructor_SetsProtocol()
        {
            var imposter = new Imposter(123, Protocol.Http);
            Assert.AreEqual("http", imposter.Protocol);
        }

        [TestMethod]
        public void Constructor_PendingSubmissionUponCreation()
        {
            var imposter = new Imposter(123, Protocol.Http);
            Assert.IsTrue(imposter.PendingSubmission);
        }

        #endregion

        #region Stub Tests

        [TestMethod]
        public void AddStub_AddsStubToCollection()
        {
            var imposter = new Imposter(123, Protocol.Http);
            imposter.AddStub();
            Assert.AreEqual(1, imposter.Stubs.Count);
        }

        #endregion
    }
}
