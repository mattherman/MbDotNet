using MbDotNet.Models.Responses;
using Xunit;

namespace MbDotNet.Tests.Models.Responses
{
	[Trait("Category", "Unit")]
	public class FaultResponseTests
	{
		[Fact]
		public void FaultResponse_Constructor_SetsFault()
		{
			const Fault fault = Fault.RandomDataThenClose;
			var response = new FaultResponse(fault);
			Assert.Equal(fault, response.Fault);
		}

		[Fact]
		public void FaultResponse_Constructor_SetsBehaviors()
		{
			var behavior = new WaitBehavior(1000);
			var response = new FaultResponse(Fault.ConnectionResetByPeer, new []{ behavior });
			Assert.Single(response.Behaviors);
			Assert.Same(behavior, response.Behaviors[0]);
		}
	}
}

