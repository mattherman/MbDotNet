using System.Collections.Generic;
using MbDotNet.Models.Predicates;
using Xunit;

namespace MbDotNet.Tests.Models.Predicates
{
	[Trait("Category", "Unit")]
	public class XPathSelectorTests
	{
		[Fact]
		public void XPathSelector_Constructor_SetsSelector()
		{
			const string expectedSelector = "//Test";

			var selector = new XPathSelector(expectedSelector);
			Assert.Equal(expectedSelector, selector.Selector);
		}

		[Fact]
		public void XPathSelector_Constructor_WithNamespace_SetsSelector()
		{
			const string expectedSelector = "//Test";

			var selector = new XPathSelector(expectedSelector, null);
			Assert.Equal(expectedSelector, selector.Selector);
		}

		[Fact]
		public void XPathSelector_Constructor_WithNamespace_SetsNamespaceDictionary()
		{
			var namespaces = new Dictionary<string, string> { { "isbn", "http://xmlnamespaces.org/isbn" } };

			var selector = new XPathSelector("//isbn:book", namespaces);
			Assert.Equal(namespaces, selector.Namespaces);
		}
	}
}
