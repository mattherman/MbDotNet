using MbDotNet.Models.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Predicates
{
    [TestClass]
    public class NotPredicateTests : PredicateTestBase
    {
        [TestMethod]
        public void Constructor_SetsChildPredicate()
        {
            var expectedChildPredicate = new EqualsPredicate<TestPredicateFields>(new TestPredicateFields());

            var predicate = new NotPredicate(expectedChildPredicate);

            Assert.AreEqual(expectedChildPredicate, predicate.ChildPredicate);
        }
    }
}
