using System.Collections.Generic;
using MbDotNet.Models.Predicates;
using Xunit;

namespace MbDotNet.Tests.Models.Predicates
{
	[Trait("Category", "Unit")]
	public class AndPredicateTests : PredicateTestBase
	{
		[Fact]
		public void AndPredicate_Constructor_SetsPredicateCollection()
		{
			var expectedPredicates = new List<Predicate>
			{
				new EqualsPredicate<TestPredicateFields>(null),
				new MatchesPredicate<TestPredicateFields>(null),
			};
			var predicate = new AndPredicate(expectedPredicates);
			Assert.Same(expectedPredicates, predicate.Predicates);
		}
	}
}
