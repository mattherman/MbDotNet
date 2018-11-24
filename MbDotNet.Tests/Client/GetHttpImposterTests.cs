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
        public void NonHttpImposterRetrieved_ThrowsInvalidProtocolException()
        {
            const int port = 6000;
            MockRequestProxy.Setup(x => x.GetHttpImposterAsync(port)).Returns(new RetrievedHttpImposter {Protocol = "Tcp"});

            Client.GetHttpImposterAsync(port);
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

            MockRequestProxy.Setup(x => x.GetHttpImposterAsync(port)).Returns(expectedImposter);

            var result = Client.GetHttpImposterAsync(port);

            Assert.AreSame(expectedImposter, result);
        }
    }
}
