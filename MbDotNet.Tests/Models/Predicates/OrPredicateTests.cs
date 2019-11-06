using MbDotNet.Models.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MbDotNet.Tests.Models.Predicates
{
    [TestClass, TestCategory("Unit")]
    public class OrPredicateTests : PredicateTestBase
    {
        [TestMethod]
        public void OrPredicate_Constructor_SetsPredicateCollection()
        {
            var expectedPredicates = new List<PredicateBase>
            {
                new EqualsPredicate<TestPredicateFields>(null),
                new MatchesPredicate<TestPredicateFields>(null),
            };
            var predicate = new OrPredicate(expectedPredicates);
            Assert.AreSame(expectedPredicates, predicate.Predicates);
        }
    }
}
