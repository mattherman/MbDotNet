using MbDotNet.Models.Predicates;
using Xunit;

namespace MbDotNet.Tests.Models.Predicates
{
	[Trait("Category", "Unit")]
	public class NotPredicateTests : PredicateTestBase
	{
		[Fact]
		public void NotPredicate_Constructor_SetsChildPredicate()
		{
			var expectedChildPredicate = new EqualsPredicate<TestPredicateFields>(new TestPredicateFields());

			var predicate = new NotPredicate(expectedChildPredicate);

			Assert.Equal(expectedChildPredicate, predicate.ChildPredicate);
		}
	}
}
