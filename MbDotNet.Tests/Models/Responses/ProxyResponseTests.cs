using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Responses
{
    [TestClass, TestCategory("Unit")]
    public class ProxyResponseTests
    {
        private class TestResponseFields : ResponseFields { }

        [TestMethod]
        public void TestResponse_Constructor_SetsFields()
        {
            var expectedFields = new TestResponseFields();
            var response = new ProxyResponse<TestResponseFields>(expectedFields);
            Assert.AreSame(expectedFields, response.Fields);
        }

        [TestMethod]
        public void TestResponse_Constructor_SetsBehavior()
        {
            var behavior = new Behavior();
            var response = new ProxyResponse<TestResponseFields>(new TestResponseFields(), behavior);
            Assert.AreSame(behavior, response.Behavior);
        }
    }
}
