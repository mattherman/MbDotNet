using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace MbDotNet.Tests.Client
{
    [TestClass, TestCategory("Unit")]
    public class GetHttpsImposterTests : MountebankClientTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidProtocolException))]
        public async Task NonHttpsImposterRetrieved_ThrowsInvalidProtocolException()
        {
            const int port = 6000;
            MockRequestProxy.Setup(x => x.GetHttpsImposterAsync(port, default)).ReturnsAsync(new RetrievedHttpsImposter() { Protocol = "Tcp" });

            await Client.GetHttpsImposterAsync(port).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task HttpsImposterRetrieved_ReturnsImposter()
        {
            const int port = 6000;
            var expectedImposter = new RetrievedHttpsImposter
            {
                Port = port,
                Protocol = "Https"
            };

            MockRequestProxy.Setup(x => x.GetHttpsImposterAsync(port, default)).ReturnsAsync(expectedImposter);

            var result = await Client.GetHttpsImposterAsync(port).ConfigureAwait(false);

            Assert.AreSame(expectedImposter, result);
        }
    }
}
