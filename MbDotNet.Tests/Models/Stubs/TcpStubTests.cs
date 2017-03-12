using System.Linq;
using MbDotNet.Models.Stubs;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MbDotNet.Tests.Models.Stubs
{
    [TestClass]
    public class TcpStubTests
    {
        [TestMethod]
        public void Constructor_InitializesResponsesCollection()
        {
            var stub = new TcpStub();
            Assert.IsNotNull(stub.Responses);
        }

        [TestMethod]
        public void Constructor_InitializesPredicatesCollection()
        {
            var stub = new TcpStub();
            Assert.IsNotNull(stub.Predicates);
        }

        [TestMethod]
        public void ReturnsData_AddsResponse_DataSet()
        {
            const string expectedData = "TestData";

            var stub = new TcpStub();
            stub.ReturnsData(expectedData);

            var response = stub.Responses.First() as IsResponse<TcpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedData, response.Fields.Data);
        }

        [TestMethod]
        public void Returns_AddsResponse_SetsFields()
        {
            var expectedFields = new TcpResponseFields
            {
                Data = "TestData"
            };

            var stub = new TcpStub();
            stub.Returns(new IsResponse<TcpResponseFields>(expectedFields));

            var response = stub.Responses.First() as IsResponse<TcpResponseFields>;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedFields, response.Fields);
        }

        [TestMethod]
        public void OnDataEquals_AddsPredicate_DataSet()
        {
            const string expectedData = "TestData";

            var stub = new TcpStub();
            stub.OnDataEquals(expectedData);

            var predicate = stub.Predicates.First() as EqualsPredicate<TcpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedData, predicate.Fields.Data);
        }

        [TestMethod]
        public void On_AddsPredicate_SetsFields()
        {
            var expectedFields = new TcpPredicateFields
            {
                Data = "TestData",
                RequestFrom = "socket"
            };

            var stub = new TcpStub();
            stub.On(new EqualsPredicate<TcpPredicateFields>(expectedFields));

            var predicate = stub.Predicates.First() as EqualsPredicate<TcpPredicateFields>;
            Assert.IsNotNull(predicate);
            Assert.AreEqual(expectedFields, predicate.Fields);
        }
    }
}
