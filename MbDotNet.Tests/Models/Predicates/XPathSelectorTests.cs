using System.Collections.Generic;
using MbDotNet.Models.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Predicates
{
    [TestClass, TestCategory("Unit")]
    public class XPathSelectorTests
    {
        [TestMethod]
        public void XPathSelector_Constructor_SetsSelector()
        {
            const string expectedSelector = "//Test";

            var selector = new XPathSelector(expectedSelector);
            Assert.AreEqual(expectedSelector, selector.Selector);
        }

        [TestMethod]
        public void XPathSelector_Constructor_WithNamespace_SetsSelector()
        {
            const string expectedSelector = "//Test";

            var selector = new XPathSelector(expectedSelector, null);
            Assert.AreEqual(expectedSelector, selector.Selector);
        }

        [TestMethod]
        public void XPathSelector_Constructor_WithNamespace_SetsNamespaceDictionary()
        {
            var namespaces = new Dictionary<string, string> {{"isbn", "http://xmlnamespaces.org/isbn"}};

            var selector = new XPathSelector("//isbn:book", namespaces);
            Assert.AreEqual(namespaces, selector.Namespaces);
        }
    }
}
