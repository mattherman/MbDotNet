using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
    [TestClass, TestCategory("Unit")]
    public class CreateHttpImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        public void HttpImposter_ShouldNotAddNewImposterToCollection()
        {
            Client.CreateHttpImposter(123);
            Assert.AreEqual(0, Client.Imposters.Count);
        }
        
        [TestMethod]
        public void HttpImposter_WithoutName_SetsNameToNull()
        {
            var imposter = Client.CreateHttpImposter(123);
            
            Assert.IsNotNull(imposter);
            Assert.IsNull(imposter.Name);
        }

        [TestMethod]
        public void HttpImposter_WithName_SetsName()
        {
            const string expectedName = "Service";

            var imposter = Client.CreateHttpImposter(123, expectedName);
            
            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedName, imposter.Name);
        }        

        [TestMethod]
        public void HttpImposter_WithoutPortAndName_SetsPortAndNameToNull()
        {
            var imposter = Client.CreateHttpImposter();

            Assert.IsNotNull(imposter);
            Assert.AreEqual(default(int), imposter.Port);
            Assert.IsNull(imposter.Name);
        }

        [TestMethod]
        public void HttpImposter_WithoutRecordRequests_SetsRecordRequest()
        {
            var imposter = Client.CreateHttpImposter(123, "service");

            Assert.IsFalse(imposter.RecordRequests);
        }

        [TestMethod]
        public void HttpImposter_WithRecordRequests_SetsRecordRequest()
        {
            const bool recordRequests = true;

            var imposter = Client.CreateHttpImposter(123, "service", recordRequests);

            Assert.IsTrue(imposter.RecordRequests);
        }

        [TestMethod]
        public void HttpImposter_WithoutDefaultRequest_SetsDefaultRequest()
        {
            var imposter = Client.CreateHttpImposter(123, "service");

            Assert.IsNull(imposter.DefaultResponse);
        }

        [TestMethod]
        public void HttpImposter_WithDefaultRequest_SetsDefaultRequest()
        {
            var defaultResponse = new HttpResponseFields();
            var imposter = Client.CreateHttpImposter(123, "service", defaultResponse: defaultResponse);

            Assert.IsNotNull(imposter.DefaultResponse);
            Assert.AreEqual(defaultResponse, imposter.DefaultResponse);
        }
    }
}