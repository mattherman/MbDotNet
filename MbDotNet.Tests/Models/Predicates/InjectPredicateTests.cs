using MbDotNet.Models.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Predicates
{
    [TestClass, TestCategory("Unit")]
    public class InjectPredicateTests : PredicateTestBase
    {
        [TestMethod]
        public void InjectPredicate_Constructor_SetsInjectedFunction()
        {
            const string injectedFunction = "function(config) { return true; }";
            var predicate = new InjectPredicate(injectedFunction);
            Assert.AreEqual(injectedFunction, predicate.InjectedFunction);
        }
    }
}