using Moq;

namespace MbDotNet.Tests.Client
{
	public class MountebankClientTestBase
	{
		protected IClient Client;
		internal Mock<IRequestProxy> MockRequestProxy;

		public MountebankClientTestBase()
		{
			MockRequestProxy = new Mock<IRequestProxy>();
			Client = new MountebankClient(MockRequestProxy.Object);
		}
	}
}
