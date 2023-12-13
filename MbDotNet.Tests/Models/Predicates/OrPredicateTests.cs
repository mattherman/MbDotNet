using System.Collections.Generic;
using MbDotNet.Models.Predicates;
using Xunit;

namespace MbDotNet.Tests.Models.Predicates
{
	[Trait("Category", "Unit")]
	public class OrPredicateTests : PredicateTestBase
	{
		[Fact]
		public void OrPredicate_Constructor_SetsPredicateCollection()
		{
			var expectedPredicates = new List<Predicate>
			{
				new EqualsPredicate<TestPredicateFields>(null),
				new MatchesPredicate<TestPredicateFields>(null),
			};
			var predicate = new OrPredicate(expectedPredicates);
			Assert.Same(expectedPredicates, predicate.Predicates);
		}
	}
}
