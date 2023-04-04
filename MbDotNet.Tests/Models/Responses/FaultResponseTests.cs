using MbDotNet.Models.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Responses
{
	[TestClass, TestCategory("Unit")]
	public class FaultResponseTests
	{
		[TestMethod]
		public void FaultResponse_Constructor_SetsFault()
		{
			const Fault fault = Fault.RandomDataThenClose;
			var response = new FaultResponse(fault);
			Assert.AreEqual(fault, response.Fault);
		}

		[TestMethod]
		public void FaultResponse_Constructor_SetsBehaviors()
		{
			var behavior = new WaitBehavior(1000);
			var response = new FaultResponse(Fault.ConnectionResetByPeer, new []{ behavior });
			Assert.AreEqual(1, response.Behaviors.Count);
			Assert.AreSame(behavior, response.Behaviors[0]);
		}
	}
}

