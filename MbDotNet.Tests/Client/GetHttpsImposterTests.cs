using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
    [TestClass]
    public class GetHttpsImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidProtocolException))]
        public void NonHttpsImposterRetrieved_ThrowsInvalidProtocolException()
        {
            const int port = 6000;
            MockRequestProxy.Setup(x => x.GetHttpsImposter(port)).Returns(new RetrievedHttpsImposter() { Protocol = "Tcp" });

            Client.GetHttpsImposter(port);
        }

        [TestMethod]
        public void HttpsImposterRetrieved_ReturnsImposter()
        {
            const int port = 6000;
            var expectedImposter = new RetrievedHttpsImposter
            {
                Port = port,
                Protocol = "Https"
            };

            MockRequestProxy.Setup(x => x.GetHttpsImposter(port)).Returns(expectedImposter);

            var result = Client.GetHttpsImposter(port);

            Assert.AreSame(expectedImposter, result);
        }
    }
}
