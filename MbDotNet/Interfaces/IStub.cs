using System.Collections.Generic;
using System.Net;
using MbDotNet.Enums;

namespace MbDotNet.Interfaces
{
    public interface IStub
    {
        /// <summary>
        /// A collection of all of the responses set up on this stub.
        /// </summary>
        ICollection<IResponse> Responses { get; }

        /// <summary>
        /// A collection of all of the predicates set up on this stub.
        /// </summary>
        ICollection<IPredicate> Predicates { get; }

        /// <summary>
        /// Adds a response to the stub that will return the specified HTTP status code.
        /// </summary>
        /// <param name="statusCode">The status code to be returned</param>
        /// <returns>The stub that the response was added to</returns>
        IStub ReturnsStatus(HttpStatusCode statusCode);

        /// <summary>
        /// Adds a response to the stub that will return the specified HTTP status code
        /// along with a response object serialized as JSON. Automatically adds an appropriate
        /// Content-Type header to the response.
        /// </summary>
        /// <typeparam name="T">The type of the response object being serialized</typeparam>
        /// <param name="statusCode">The status code to be returned</param>
        /// <param name="responseObject">The response object of type T that will be returned as JSON</param>
        /// <returns>The stub that the response was added to</returns>
        IStub ReturnsJson<T>(HttpStatusCode statusCode, T responseObject);

        /// <summary>
        /// Adds a response to the stub that will return the specified HTTP status code
        /// along with a response object serialized as XML. Automatically adds an appropriate
        /// Content-Type header to the response.
        /// </summary>
        /// <typeparam name="T">The type of the response object being serialized</typeparam>
        /// <param name="statusCode">The status code to be returned</param>
        /// <param name="responseObject">The response object of type T that will be returned as XML</param>
        /// <returns>The stub that the response was added to</returns>
        IStub ReturnsXml<T>(HttpStatusCode statusCode, T responseObject);

        /// <summary>
        /// Adds a response to the stub with the specified content type
        /// </summary>
        /// <param name="statusCode">The status code to be returned</param>
        /// <param name="headers">The headers for the response</param>
        /// <param name="responseObject">The response object that will be returned as the specified content type</param>
        /// <returns></returns>
        IStub Returns(HttpStatusCode statusCode, IDictionary<string, string> headers, object responseObject);

        /// <summary>
        /// Adds a response to the stub.
        /// </summary>
        /// <param name="response">The response object designating what the stub will return</param>
        /// <returns>The stub that the response was added to</returns>
        IStub Returns(IResponse response);

        /// <summary>
        /// Adds a predicate to the stub that will match when the request path equals the specified path.
        /// </summary>
        /// <param name="path">The path to match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        IStub OnPathEquals(string path);

        /// <summary>
        /// Adds a predicate to the stub that will match when the request path equals the specified path
        /// and the method of the request matches the specified HTTP method.
        /// </summary>
        /// <param name="path">The path to match on</param>
        /// <param name="method">The method to match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        IStub OnPathAndMethodEqual(string path, Method method);

        /// <summary>
        /// Adds a predicate to the stub
        /// </summary>
        /// <param name="predicate">The predicate object designating what the stub will match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        IStub On(IPredicate predicate);
    }
}
