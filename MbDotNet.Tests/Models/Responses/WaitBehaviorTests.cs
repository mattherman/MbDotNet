using MbDotNet.Models.Responses;
using Xunit;

namespace MbDotNet.Tests.Models.Responses
{
	[Trait("Category", "Unit")]
	public class WaitBehaviorTests
	{
		[Fact]
		public void WaitBehavior_Constructor_SetsLatency()
		{
			const int latencyInMilliseconds = 1500;
			var behavior = new WaitBehavior(latencyInMilliseconds);
			Assert.Equal(latencyInMilliseconds, behavior.LatencyInMilliseconds);
		}
	}
}
