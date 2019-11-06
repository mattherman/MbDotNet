using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Responses
{
    [TestClass, TestCategory("Unit")]
    public class IsResponseTests
    {
        private class TestResponseFields : ResponseFields { }

        [TestMethod]
        public void TestResponse_Constructor_SetsFields()
        {
            var expectedFields = new TestResponseFields();
            var response = new IsResponse<TestResponseFields>(expectedFields);
            Assert.AreSame(expectedFields, response.Fields);
        }
    }
}
