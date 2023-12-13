using System.Threading.Tasks;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Moq;
using Xunit;

using MSAssert = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class GetSmtpImposterTests : MountebankClientTestBase
	{
		[Fact]
		public async Task NonSmtpImposterRetrieved_ThrowsInvalidProtocolException()
		{
			const int port = 6000;
			MockRequestProxy.Setup(x => x.GetSmtpImposterAsync(port, default)).ReturnsAsync(new RetrievedSmtpImposter { Protocol = "Http" });

			await Assert.ThrowsAsync<InvalidProtocolException>(async () =>
			{
				await Client.GetSmtpImposterAsync(port);
			});
		}

		[Fact]
		public async Task SmtpImposterRetrieved_ReturnsImposter()
		{
			const int port = 6000;
			var expectedImposter = new RetrievedSmtpImposter
			{
				Port = port,
				Protocol = "Smtp"
			};

			MockRequestProxy.Setup(x => x.GetSmtpImposterAsync(port, default)).ReturnsAsync(expectedImposter);

			var result = await Client.GetSmtpImposterAsync(port);

			Assert.Same(expectedImposter, result);
		}
	}
}
