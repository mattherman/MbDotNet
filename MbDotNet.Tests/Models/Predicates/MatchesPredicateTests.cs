using MbDotNet.Models.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Predicates
{
    [TestClass, TestCategory("Unit")]
    public class MatchesPredicateTests : PredicateTestBase
    {
        [TestMethod]
        public void MatchesPredicate_Constructor_SetsFieldObject()
        {
            var expectedFields = new TestPredicateFields();
            var predicate = new MatchesPredicate<TestPredicateFields>(expectedFields);
            Assert.AreSame(expectedFields, predicate.Fields);
        }

        [TestMethod]
        public void MatchesPredicate_Constructor_SetsCaseSensitivity()
        {
            var fields = new TestPredicateFields();
            var predicate = new MatchesPredicate<TestPredicateFields>(fields, isCaseSensitive: true);
            Assert.IsTrue(predicate.IsCaseSensitive);
        }

        [TestMethod]
        public void MatchesPredicate_Constructor_SetsExceptExpression()
        {
            const string expectedExceptRegex = "!$";

            var fields = new TestPredicateFields();
            var predicate = new MatchesPredicate<TestPredicateFields>(fields, exceptExpression: expectedExceptRegex);
            Assert.AreEqual(expectedExceptRegex, predicate.ExceptExpression);
        }

        [TestMethod]
        public void MatchesPredicate_Constructor_SetsXpathSelector()
        {
            var expectedXPathSelector = new XPathSelector("!$");

            var fields = new TestPredicateFields();
            var predicate = new MatchesPredicate<TestPredicateFields>(fields, xpath: expectedXPathSelector);
            Assert.AreEqual(expectedXPathSelector, predicate.XPathSelector);
        }

        [TestMethod]
        public void MatchesPredicate_Constructor_SetsJsonPathSelector()
        {
            var expectedJsonPathSelector = new JsonPathSelector("$..title");

            var fields = new TestPredicateFields();
            var predicate = new MatchesPredicate<TestPredicateFields>(fields, jsonpath: expectedJsonPathSelector);
            Assert.AreEqual(expectedJsonPathSelector, predicate.JsonPathSelector);
        }
    }
}
