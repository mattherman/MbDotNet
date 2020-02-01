using System.Linq;
using System.Threading.Tasks;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MbDotNet.Tests.Client
{
    [TestClass, TestCategory("Unit")]
    public class SubmitTests : MountebankClientTestBase
    {
        [TestMethod]
        public async Task Submit_CallsSubmitOnAllPendingImposters()
        {
            const int firstPortNumber = 123;
            const int secondPortNumber = 456;

            var imposter1 = new HttpImposter(firstPortNumber, null);
            var imposter2 = new HttpImposter(secondPortNumber, null);

            await Client.SubmitAsync(new[] { imposter1, imposter2 }).ConfigureAwait(false);

            MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == firstPortNumber), default), Times.Once);
            MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == secondPortNumber), default), Times.Once);
        }

        [TestMethod]
        public async Task SubmitCollection_ShouldSubmitImpostersUsingProxy()
        {
            const int firstPortNumber = 123;
            const int secondPortNumber = 456;

            var imposter1 = Client.CreateHttpImposter(firstPortNumber);
            var imposter2 = Client.CreateHttpImposter(secondPortNumber);

            await Client.SubmitAsync(new[] { imposter1, imposter2 }).ConfigureAwait(false);

            MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == firstPortNumber), default), Times.Once);
            MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == secondPortNumber), default), Times.Once);
        }

        [TestMethod]
        public async Task SubmitCollection_ShouldAddImpostersToCollection()
        {
            const int firstPortNumber = 123;
            const int secondPortNumber = 456;

            var imposter1 = Client.CreateHttpImposter(firstPortNumber);
            var imposter2 = Client.CreateHttpImposter(secondPortNumber);

            await Client.SubmitAsync(new[] { imposter1, imposter2 }).ConfigureAwait(false);

            Assert.AreEqual(1, Client.Imposters.Count(x => x.Port == firstPortNumber));
            Assert.AreEqual(1, Client.Imposters.Count(x => x.Port == secondPortNumber));
        }

        [TestMethod]
        public async Task Submit_AllowsNullPort()
        {
            var imposter = new HttpImposter(null, null);

            await Client.SubmitAsync(imposter).ConfigureAwait(false);

            MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == default), default), Times.Once);
        }
    }
}
