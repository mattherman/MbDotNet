using MbDotNet.Models.Predicates;
using Xunit;

namespace MbDotNet.Tests.Models.Predicates
{
	[Trait("Category", "Unit")]
	public class InjectPredicateTests : PredicateTestBase
	{
		[Fact]
		public void InjectPredicate_Constructor_SetsInjectedFunction()
		{
			const string injectedFunction = "function(config) { return true; }";
			var predicate = new InjectPredicate(injectedFunction);
			Assert.Equal(injectedFunction, predicate.InjectedFunction);
		}
	}
}
