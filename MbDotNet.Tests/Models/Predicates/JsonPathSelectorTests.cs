using MbDotNet.Models.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Predicates
{
    [TestClass]
    public class JsonPathSelectorTests
    {
        [TestMethod]
        public void JsonPathSelector_Constructor_SetsSelector()
        {
            const string expectedSelector = "$..title";

            var selector = new JsonPathSelector(expectedSelector);
            Assert.AreEqual(expectedSelector, selector.Selector);
        }
    }
}
