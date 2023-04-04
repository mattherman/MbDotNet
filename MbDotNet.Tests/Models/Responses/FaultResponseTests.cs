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
		public void FaultResponse_Constructor_SetsBehavior()
		{
			var behavior = new Behavior();
			var response = new FaultResponse(Fault.ConnectionResetByPeer, behavior);
			Assert.AreSame(behavior, response);
		}
	}
}

