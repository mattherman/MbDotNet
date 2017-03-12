using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void NonHttpImposterRetrieved_ThrowsInvalidProtocolException()
        {
            const int port = 6000;
            MockRequestProxy.Setup(x => x.GetHttpsImposter(port)).Returns(new RetrievedHttpsImposter() { Protocol = "Tcp" });

            Client.GetHttpsImposter(port);
        }

        [TestMethod]
        public void HttpImposterRetrieved_ReturnsImposter()
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
