using System.Collections.Generic;
using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Predicates
{
    [TestClass]
    public class EqualsPredicateTests
    {
        [TestMethod]
        public void Constructor_SetsPath()
        {
            const string expectedPath = "/test";

            var predicate = new EqualsPredicate(expectedPath, null, null, null, null);
            Assert.AreEqual(expectedPath, predicate.Path);
        }

        [TestMethod]
        public void Constructor_NullMethod_SetsMethodToNull()
        {
            var predicate = new EqualsPredicate(null, null, null, null, null);
            Assert.IsNull(predicate.Method);
        }

        [TestMethod]
        public void Constructor_NonNullMethod_SetsMethodUppercase()
        {
            const string expectedMethodString = "POST";

            var predicate = new EqualsPredicate(null, Method.Post, null, null, null);
            Assert.AreEqual(expectedMethodString, predicate.Method);
        }

        [TestMethod]
        public void Constructor_SetsRequestBody()
        {
            const string expectedBody = "test";

            var predicate = new EqualsPredicate(null, null, expectedBody, null, null);
            Assert.AreEqual(expectedBody, predicate.RequestBody);
        }

        [TestMethod]
        public void Constructor_SetsHeaders()
        {
            var predicate = new EqualsPredicate(null, null, null, new Dictionary<string, string>(), null);
            Assert.IsNotNull(predicate.Headers);
        }

        [TestMethod]
        public void Constructor_SetsQueryParameters()
        {
            var predicate = new EqualsPredicate(null, null, null, null, new Dictionary<string, string>());
            Assert.IsNotNull(predicate.QueryParameters);
        }
    }
}
