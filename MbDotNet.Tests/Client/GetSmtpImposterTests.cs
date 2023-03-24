using System.Threading.Tasks;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MbDotNet.Tests.Client
{
	[TestClass, TestCategory("Unit")]
	public class GetSmtpImposterTests : MountebankClientTestBase
	{
		[TestMethod]
		[ExpectedException(typeof(InvalidProtocolException))]
		public async Task NonSmtpImposterRetrieved_ThrowsInvalidProtocolException()
		{
			const int port = 6000;
			MockRequestProxy.Setup(x => x.GetSmtpImposterAsync(port, default)).ReturnsAsync(new RetrievedSmtpImposter { Protocol = "Http" });

			await Client.GetSmtpImposterAsync(port).ConfigureAwait(false);
		}

		[TestMethod]
		public async Task SmtpImposterRetrieved_ReturnsImposter()
		{
			const int port = 6000;
			var expectedImposter = new RetrievedSmtpImposter
			{
				Port = port,
				Protocol = "Smtp"
			};

			MockRequestProxy.Setup(x => x.GetSmtpImposterAsync(port, default)).ReturnsAsync(expectedImposter);

			var result = await Client.GetSmtpImposterAsync(port).ConfigureAwait(false);

			Assert.AreSame(expectedImposter, result);
		}
	}
}
