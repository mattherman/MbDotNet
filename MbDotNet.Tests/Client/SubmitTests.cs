using System.Threading.Tasks;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MbDotNet.Tests.Client
{
	[TestClass, TestCategory("Unit")]
	public class SubmitTests : MountebankClientTestBase
	{
		[TestMethod]
		public async Task Submit_CallsSubmitOnAllPendingImposters()
		{
			const int firstPortNumber = 123;
			const int secondPortNumber = 456;

			var imposter1 = new HttpImposter(firstPortNumber, null);
			var imposter2 = new HttpImposter(secondPortNumber, null);

			await Client.SubmitAsync(new[] { imposter1, imposter2 }).ConfigureAwait(false);

			MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == firstPortNumber), default), Times.Once);
			MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == secondPortNumber), default), Times.Once);
		}

		[TestMethod]
		public async Task SubmitCollection_ShouldSubmitImpostersUsingProxy()
		{
			const int firstPortNumber = 123;
			const int secondPortNumber = 456;

			await Client.CreateTcpImposterAsync(firstPortNumber, _ => { });
			await Client.CreateTcpImposterAsync(secondPortNumber, _ => { });

			MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == firstPortNumber), default), Times.Once);
			MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == secondPortNumber), default), Times.Once);
		}

		[TestMethod]
		public async Task Submit_AllowsNullPort()
		{
			var imposter = new HttpImposter(null, null);

			await Client.SubmitAsync(imposter).ConfigureAwait(false);

			MockRequestProxy.Verify(x => x.CreateImposterAsync(It.Is<Imposter>(imp => imp.Port == default), default), Times.Once);
		}
	}
}
