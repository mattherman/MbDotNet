using MbDotNet.Models.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Predicates
{
    [TestClass, TestCategory("Unit")]
    public class InjectPredicateTests : PredicateTestBase
    {
        [TestMethod]
        public void InjectPredicate_Constructor_SetsJavaScriptFunction()
        {
            const string javaScriptFunction = "function(config) { return true; }";
            var predicate = new InjectPredicate(javaScriptFunction);
            Assert.AreEqual(javaScriptFunction, predicate.JavaScriptFunction);
        }
    }
}