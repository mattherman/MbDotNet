using System;
using Moq;
using Xunit;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class DisposeClientTests
	{
		[Fact]
		public void Dispose_DisposesRequestProxy()
		{
			var mockRequestProxy = new Mock<IRequestProxy>();
			var mockDisposable = mockRequestProxy.As<IDisposable>();
			var client = new MountebankClient(mockRequestProxy.Object);

			client.Dispose();

			mockDisposable.Verify(x => x.Dispose(), Times.Once);
		}

		[Fact]
		public void Dispose_CalledTwice_DisposesRequestProxyOnce()
		{
			var mockRequestProxy = new Mock<IRequestProxy>();
			var mockDisposable = mockRequestProxy.As<IDisposable>();
			var client = new MountebankClient(mockRequestProxy.Object);

			client.Dispose();
			client.Dispose();

			mockDisposable.Verify(x => x.Dispose(), Times.Once);
		}

		[Fact]
		public void Dispose_RequestProxyIsNotDisposable_DoesNotThrow()
		{
			var client = new MountebankClient(new Mock<IRequestProxy>().Object);

			client.Dispose();
		}
	}
}
