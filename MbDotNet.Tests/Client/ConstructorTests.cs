using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
    [TestClass, TestCategory("Unit")]
    public class ConstructorTests : MountebankClientTestBase
    {
        [TestMethod]
        public void InitializesImposterCollection()
        {
            var client = new MountebankClient();
            Assert.IsNotNull(client.Imposters);
        }
    }
}
