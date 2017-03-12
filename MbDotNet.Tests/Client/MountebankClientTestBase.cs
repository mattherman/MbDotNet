using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace MbDotNet.Tests.Client
{
    public class MountebankClientTestBase
    {
        protected IClient Client;
        internal Mock<IRequestProxy> MockRequestProxy;

        [TestInitialize]
        public void TestInitialize()
        {
            MockRequestProxy = new Mock<IRequestProxy>();
            Client = new MountebankClient(MockRequestProxy.Object);
        }
    }
}