using System.Linq;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class SubmitTests : MountebankClientTestBase
    {
        [TestMethod]
        public void Submit_CallsSubmitOnAllPendingImposters()
        {
            const int firstPortNumber = 123;
            const int secondPortNumber = 456;

            var imposter1 = new HttpImposter(firstPortNumber, null);
            var imposter2 = new HttpImposter(secondPortNumber, null);

            Client.SubmitAsync(new[] { imposter1, imposter2 });

            MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == firstPortNumber)), Times.Once);
            MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == secondPortNumber)), Times.Once);
        }

        [TestMethod]
        public void SubmitCollection_ShouldSubmitImpostersUsingProxy()
        {
            const int firstPortNumber = 123;
            const int secondPortNumber = 456;

            var imposter1 = Client.CreateHttpImposter(firstPortNumber);
            var imposter2 = Client.CreateHttpImposter(secondPortNumber);

            Client.SubmitAsync(new[] { imposter1, imposter2 });

            MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == firstPortNumber)), Times.Once);
            MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == secondPortNumber)), Times.Once);
        }

        [TestMethod]
        public void SubmitCollection_ShouldAddImpostersToCollection()
        {
            const int firstPortNumber = 123;
            const int secondPortNumber = 456;

            var imposter1 = Client.CreateHttpImposter(firstPortNumber);
            var imposter2 = Client.CreateHttpImposter(secondPortNumber);

            Client.SubmitAsync(new[] { imposter1, imposter2 });

            Assert.AreEqual(1, Client.Imposters.Count(x => x.Port == firstPortNumber));
            Assert.AreEqual(1, Client.Imposters.Count(x => x.Port == secondPortNumber));
        }

        [TestMethod]
        public void Submit_AllowsNullPort()
        {
            var imposter = new HttpImposter(null, null);

            Client.SubmitAsync(imposter);

            MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == default(int))), Times.Once);
        }
    }
}
