using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MbDotNet.Models.Responses;
using Moq;
using Xunit;

namespace MbDotNet.Tests.Client
{
	[Trait("Category", "Unit")]
	public class GetLogsTest : MountebankClientTestBase
	{
		[Fact]

		public async Task ReturnsLogs()
		{

			var expectedResult = new List<Log>
			{
				new Log { Level = "info", Message = "[mb:2525] DELETE /imposters", Timestamp = new DateTime(2022, 9, 21, 5, 0, 0) },
				new Log { Level = "warn", Message = "", Timestamp = new DateTime(2022, 9, 21, 5, 0, 1) },
				new Log { Level = "info", Message = "[mb:2525] GET /imposters", Timestamp = new DateTime(2022, 9, 21, 5, 0, 2) }
			};


			MockRequestProxy.Setup(x => x.GetLogsAsync(default)).ReturnsAsync(expectedResult);
			var result = await Client.GetLogsAsync();

			Assert.Same(expectedResult, result);
		}
	}
}



