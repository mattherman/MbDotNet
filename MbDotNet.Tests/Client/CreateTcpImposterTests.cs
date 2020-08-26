using MbDotNet.Enums;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
    [TestClass, TestCategory("Unit")]
    public class CreateTcpImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        public void TcpImposter_WithoutName_SetsNameToNull()
        {
            var imposter = Client.CreateTcpImposter(123);

            Assert.IsNotNull(imposter);
            Assert.IsNull(imposter.Name);
        }

        [TestMethod]
        public void TcpImposter_WithName_SetsName()
        {
            const string expectedName = "Service";

            var imposter = Client.CreateTcpImposter(123, expectedName);
            
            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedName, imposter.Name);
        }

        [TestMethod]
        public void TcpImposter_WithoutMode_SetsModeToText()
        {
            const string expectedMode = "text";

            var imposter = Client.CreateTcpImposter(123);
            
            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedMode, imposter.Mode);
        }

        [TestMethod]
        public void TcpImposter_WithMode_SetsMode()
        {
            const string expectedMode = "binary";

            var imposter = Client.CreateTcpImposter(123, null, TcpMode.Binary);

            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedMode, imposter.Mode);
        }

        [TestMethod]
        public void TcpImposter_ShouldNotAddNewImposterToCollection()
        {
            Client.CreateTcpImposter(123);
            Assert.AreEqual(0, this.Client.Imposters.Count);
        }

        [TestMethod]
        public void TcpImposter_WithoutPortAndName_SetsPortAndNameToNull()
        {
            var imposter = Client.CreateTcpImposter();

            Assert.IsNotNull(imposter);
            Assert.AreEqual(default(int), imposter.Port);
            Assert.IsNull(imposter.Name);
        }

        [TestMethod]
        public void TcpImposter_WithoutRecordRequests_SetsRecordRequest()
        {
            var imposter = Client.CreateTcpImposter();

            Assert.IsFalse(imposter.RecordRequests);
        }

        [TestMethod]
        public void TcpImposter_WithRecordRequests_SetsRecordRequest()
        {
            const bool recordRequests = true;

            var imposter = Client.CreateTcpImposter(recordRequests: recordRequests);

            Assert.IsTrue(imposter.RecordRequests);
        }

        [TestMethod]
        public void HttpImposter_WithoutDefaultRequest_SetsDefaultRequest()
        {
            var imposter = Client.CreateTcpImposter(123, "service");

            Assert.IsNull(imposter.DefaultResponse);
        }

        [TestMethod]
        public void HttpImposter_WithDefaultRequest_SetsDefaultRequest()
        {
            var defaultResponse = new TcpResponseFields();
            var imposter = Client.CreateTcpImposter(123, "service", defaultResponse: defaultResponse);

            Assert.IsNotNull(imposter.DefaultResponse);
            Assert.AreEqual(defaultResponse, imposter.DefaultResponse);
        }
    }
}