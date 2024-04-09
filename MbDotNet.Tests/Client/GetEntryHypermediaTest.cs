using System.Threading.Tasks;
using MbDotNet.Models.Responses;
using Moq;
using Xunit;

namespace MbDotNet.Tests.Client

{
	[Trait("Category", "Unit")]
	public class GetEntryHypermediaTest : MountebankClientTestBase
	{
		[Fact]
		public async Task ReturnsEntryHypermedia()
		{

			var expectedResult = new Home()
			{

				Links = new Link()
				{
					Imposters = new HrefField(),
					Config = new HrefField(),
					Logs = new HrefField(),

				}

			};


			MockRequestProxy.Setup(x => x.GetEntryHypermediaAsync(default)).ReturnsAsync(expectedResult);
			var result = await Client.GetEntryHypermediaAsync();

			Assert.Same(expectedResult, result);
		}
	}
}



