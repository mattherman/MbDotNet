using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class GetHttpImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidProtocolException))]
        public void NonHttpImposterRetreived_ThrowsInvalidProtocolException()
        {
            const int port = 6000;
            MockRequestProxy.Setup(x => x.GetHttpImposter(port)).Returns(new RetrievedHttpImposter {Protocol = "Tcp"});

            Client.GetHttpImposter(port);
        }

        [TestMethod]
        public void HttpImposterRetrieved_ReturnsImposter()
        {
            const int port = 6000;
            var expectedImposter = new RetrievedHttpImposter
            {
                Port = port,
                Protocol = "Http"
            };

            MockRequestProxy.Setup(x => x.GetHttpImposter(port)).Returns(expectedImposter);

            var result = Client.GetHttpImposter(port);

            Assert.AreSame(expectedImposter, result);
        }
    }
}
