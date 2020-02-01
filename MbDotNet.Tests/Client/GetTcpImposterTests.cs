using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace MbDotNet.Tests.Client
{
    [TestClass, TestCategory("Unit")]
    public class GetTcpImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidProtocolException))]
        public async Task NonTcpImposterRetrieved_ThrowsInvalidProtocolException()
        {
            const int port = 6000;
            MockRequestProxy.Setup(x => x.GetTcpImposterAsync(port, default)).ReturnsAsync(new RetrievedTcpImposter { Protocol = "Http" });

            await Client.GetTcpImposterAsync(port).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task TcpImposterRetrieved_ReturnsImposter()
        {
            const int port = 6000;
            var expectedImposter = new RetrievedTcpImposter
            {
                Port = port,
                Protocol = "Tcp"
            };

            MockRequestProxy.Setup(x => x.GetTcpImposterAsync(port, default)).ReturnsAsync(expectedImposter);

            var result = await Client.GetTcpImposterAsync(port).ConfigureAwait(false);

            Assert.AreSame(expectedImposter, result);
        }
    }
}
