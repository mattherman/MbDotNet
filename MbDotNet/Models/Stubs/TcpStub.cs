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
        /// <param name="data">The data to match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        public TcpStub OnDataEquals(string data)
        {
            var fields = new TcpPredicateFields
            {
                Data = data
            };

            var predicate = new EqualsPredicate<TcpPredicateFields>(fields);

            return On(predicate);
        }

        /// <summary>
        /// Adds a predicate to the stub
        /// </summary>
        /// <param name="predicate">The predicate object designating what the stub will match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        public TcpStub On(PredicateBase predicate)
        {
            Predicates.Add(predicate);
            return this;
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

            return Returns(response);
        }

        /// <summary>
        /// Adds a response to the stub.
        /// </summary>
        /// <param name="response">The response object designating what the stub will return</param>
        /// <returns>The stub that the response was added to</returns>
        public TcpStub Returns(ResponseBase response)
        {
            Responses.Add(response);
            return this;
        }
    }
}
