using MbDotNet.Models.Predicates;
using Xunit;

namespace MbDotNet.Tests.Models.Predicates
{
	[Trait("Category", "Unit")]
	public class JsonPathSelectorTests
	{
		[Fact]
		public void JsonPathSelector_Constructor_SetsSelector()
		{
			const string expectedSelector = "$..title";

			var selector = new JsonPathSelector(expectedSelector);
			Assert.Equal(expectedSelector, selector.Selector);
		}
	}
}
