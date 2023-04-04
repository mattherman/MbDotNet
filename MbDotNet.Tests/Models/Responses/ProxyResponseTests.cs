using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Responses
{
	[TestClass, TestCategory("Unit")]
	public class ProxyResponseTests
	{
		private class TestResponseFields : ResponseFields { }

		[TestMethod]
		public void ProxyResponse_Constructor_SetsFields()
		{
			var expectedFields = new TestResponseFields();
			var response = new ProxyResponse<TestResponseFields>(expectedFields);
			Assert.AreSame(expectedFields, response.Fields);
		}

		[TestMethod]
		public void ProxyResponse_Constructor_SetsBehaviors()
		{
			var behavior = new WaitBehavior(1000);
			var response = new ProxyResponse<TestResponseFields>(new TestResponseFields(), new []{ behavior });
			Assert.AreEqual(1, response.Behaviors.Count);
			Assert.AreSame(behavior, response.Behaviors[0]);
		}
	}
}
