using MbDotNet.Models.Predicates;
using Xunit;

namespace MbDotNet.Tests.Models.Predicates
{
	[Trait("Category", "Unit")]
	public class StartsWithPredicateTests : PredicateTestBase
	{
		[Fact]
		public void StartsWithPredicate_Constructor_SetsFieldObject()
		{
			var expectedFields = new TestPredicateFields();
			var predicate = new StartsWithPredicate<TestPredicateFields>(expectedFields);
			Assert.Same(expectedFields, predicate.Fields);
		}

		[Fact]
		public void StartsWithPredicate_Constructor_SetsCaseSensitivity()
		{
			var fields = new TestPredicateFields();
			var predicate = new StartsWithPredicate<TestPredicateFields>(fields, isCaseSensitive: true);
			Assert.True(predicate.IsCaseSensitive);
		}

		[Fact]
		public void StartsWithPredicate_Constructor_SetsExceptExpression()
		{
			const string expectedExceptRegex = "!$";

			var fields = new TestPredicateFields();
			var predicate = new StartsWithPredicate<TestPredicateFields>(fields, exceptExpression: expectedExceptRegex);
			Assert.Equal(expectedExceptRegex, predicate.ExceptExpression);
		}

		[Fact]
		public void StartsWithPredicate_Constructor_SetsXpathSelector()
		{
			var expectedXPathSelector = new XPathSelector("!$");

			var fields = new TestPredicateFields();
			var predicate = new StartsWithPredicate<TestPredicateFields>(fields, xpath: expectedXPathSelector);
			Assert.Equal(expectedXPathSelector, predicate.XPathSelector);
		}

		[Fact]
		public void StartsWithPredicate_Constructor_SetsJsonPathSelector()
		{
			var expectedJsonPathSelector = new JsonPathSelector("$..title");

			var fields = new TestPredicateFields();
			var predicate = new StartsWithPredicate<TestPredicateFields>(fields, jsonpath: expectedJsonPathSelector);
			Assert.Equal(expectedJsonPathSelector, predicate.JsonPathSelector);
		}
	}
}
