using MbDotNet.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Responses
{
	[TestClass, TestCategory("Unit")]
	public class WaitBehaviorTests
	{
		[TestMethod]
		public void WaitBehavior_Constructor_SetsLatency()
		{
			const int latencyInMilliseconds = 1500;
			var behavior = new WaitBehavior(latencyInMilliseconds);
			Assert.AreEqual(latencyInMilliseconds, behavior.LatencyInMilliseconds);
		}
	}
}
