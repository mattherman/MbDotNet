using MbDotNet.Enums;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class CreateTcpImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        public void WithoutName_SetsNameToNull()
        {
            var imposter = Client.CreateTcpImposter(123);

            Assert.IsNotNull(imposter);
            Assert.IsNull(imposter.Name);
        }

        [TestMethod]
        public void WithName_SetsName()
        {
            const string expectedName = "Service";

            var imposter = Client.CreateTcpImposter(123, expectedName);
            
            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedName, imposter.Name);
        }

        [TestMethod]
        public void WithoutMode_SetsModeToText()
        {
            const string expectedMode = "text";

            var imposter = Client.CreateTcpImposter(123);
            
            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedMode, imposter.Mode);
        }

        [TestMethod]
        public void WithMode_SetsMode()
        {
            const string expectedMode = "binary";

            var imposter = Client.CreateTcpImposter(123, null, TcpMode.Binary);

            Assert.IsNotNull(imposter);
            Assert.AreEqual(expectedMode, imposter.Mode);
        }

        [TestMethod]
        public void ShouldNotAddNewImposterToCollection()
        {
            Client.CreateTcpImposter(123);
            Assert.AreEqual(0, this.Client.Imposters.Count);
        }

        [TestMethod]
        public void WithoutPortAndName_SetsPortAndNameToNull()
        {
            var imposter = Client.CreateTcpImposter();

            Assert.IsNotNull(imposter);
            Assert.AreEqual(default(int), imposter.Port);
            Assert.IsNull(imposter.Name);
        }
    }
}