using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;

namespace MbDotNet.Models.Stubs
{
    public class TcpStub : StubBase
    {

        /// <summary>
        /// Adds a predicate to the stub that will match when the request data equals the specified data.
        /// </summary>
        /// <param name="path">The data to match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        public TcpStub OnDataEquals(string data)
        {
            var fields = new TcpPredicateFields
            {
                Data = data
            };

            var predicate = new EqualsPredicate<TcpPredicateFields>(fields);

            return (TcpStub) On(predicate);
        }

        /// <summary>
        /// Adds a response to the stub that will return the specified data.
        /// </summary>
        /// <param name="data">The data to be returned</param>
        /// <returns>The stub that the response was added to</returns>
        public TcpStub ReturnsData(string data)
        {
            var fields = new TcpResponseFields
            {
                Data = data
            };

            var response = new IsResponse<TcpResponseFields>(fields);

            return (TcpStub) Returns(response);
        }
    }
}
