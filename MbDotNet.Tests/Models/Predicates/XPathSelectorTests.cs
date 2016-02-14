using System.Collections.Generic;
using MbDotNet.Models.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Predicates
{
    [TestClass]
    public class XPathSelectorTests
    {
        [TestMethod]
        public void Constructor_SetsSelector()
        {
            const string expectedSelector = "//Test";

            var selector = new XPathSelector(expectedSelector);
            Assert.AreEqual(expectedSelector, selector.Selector);
        }

        [TestMethod]
        public void Constructor_WithNamespace_SetsSelector()
        {
            const string expectedSelector = "//Test";

            var selector = new XPathSelector(expectedSelector, null);
            Assert.AreEqual(expectedSelector, selector.Selector);
        }

        [TestMethod]
        public void Constructor_WithNamespace_SetsNamespaceDictionary()
        {
            var namespaces = new Dictionary<string, string> {{"isbn", "http://xmlnamespaces.org/isbn"}};

            var selector = new XPathSelector("//isbn:book", namespaces);
            Assert.AreEqual(namespaces, selector.Namespaces);
        }
    }
}
