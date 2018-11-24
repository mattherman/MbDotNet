using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class GetTcpImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidProtocolException))]
        public void NonTcpImposterRetrieved_ThrowsInvalidProtocolException()
        {
            const int port = 6000;
            MockRequestProxy.Setup(x => x.GetTcpImposterAsync(port)).Returns(new RetrievedTcpImposter { Protocol = "Http" });

            Client.GetTcpImposterAsync(port);
        }

        [TestMethod]
        public void TcpImposterRetrieved_ReturnsImposter()
        {
            const int port = 6000;
            var expectedImposter = new RetrievedTcpImposter
            {
                Port = port,
                Protocol = "Tcp"
            };

            MockRequestProxy.Setup(x => x.GetTcpImposterAsync(port)).Returns(expectedImposter);

            var result = Client.GetTcpImposterAsync(port);

            Assert.AreSame(expectedImposter, result);
        }
    }
}
