using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace MbDotNet.Tests.Client
{
    [TestClass, TestCategory("Unit")]
    public class GetHttpImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidProtocolException))]
        public async Task NonHttpImposterRetrieved_ThrowsInvalidProtocolException()
        {
            const int port = 6000;
            MockRequestProxy.Setup(x => x.GetHttpImposterAsync(port, default)).ReturnsAsync(new RetrievedHttpImposter {Protocol = "Tcp"});

            await Client.GetHttpImposterAsync(port).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task HttpImposterRetrieved_ReturnsImposter()
        {
            const int port = 6000;
            var expectedImposter = new RetrievedHttpImposter
            {
                Port = port,
                Protocol = "Http"
            };

            MockRequestProxy.Setup(x => x.GetHttpImposterAsync(port, default)).ReturnsAsync(expectedImposter);

            var result = await Client.GetHttpImposterAsync(port).ConfigureAwait(false);

            Assert.AreSame(expectedImposter, result);
        }
    }
}
