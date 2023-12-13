using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using Xunit;

namespace MbDotNet.Tests.Models.Responses
{
	[Trait("Category", "Unit")]
	public class ProxyResponseTests
	{
		private class TestResponseFields : ResponseFields { }

		[Fact]
		public void ProxyResponse_Constructor_SetsFields()
		{
			var expectedFields = new TestResponseFields();
			var response = new ProxyResponse<TestResponseFields>(expectedFields);
			Assert.Same(expectedFields, response.Fields);
		}

		[Fact]
		public void ProxyResponse_Constructor_SetsBehaviors()
		{
			var behavior = new WaitBehavior(1000);
			var response = new ProxyResponse<TestResponseFields>(new TestResponseFields(), new []{ behavior });
			Assert.Single(response.Behaviors);
			Assert.Same(behavior, response.Behaviors[0]);
		}
	}
}
