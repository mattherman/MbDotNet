using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using System;

namespace MbDotNet.Models.Stubs
{
    public class HttpStub : StubBase
    {
        
        /// <summary>
        /// Adds a response to the stub that will return the specified HTTP status code.
        /// </summary>
        /// <param name="statusCode">The status code to be returned</param>
        /// <returns>The stub that the response was added to</returns>
        public HttpStub ReturnsStatus(HttpStatusCode statusCode)
        {
            var fields = new HttpResponseFields
            {
                StatusCode = statusCode
            };
            
            var response = new IsResponse<HttpResponseFields>(fields);

            return Returns(response);
        }

        /// <summary>
        /// Returns the specified HTTP status code along with the given string in
        /// the body of the response.
        /// </summary>
        /// <param name="statusCode">The status code to be returned</param>
        /// <param name="body">The text to include in the body of the response</param>
        /// <returns>The stub that the response was added to</returns>
        public HttpStub ReturnsBody(HttpStatusCode statusCode, string body)
        {
            return Returns(statusCode, null, body);
        }

        /// <summary>
        /// Adds a response to the stub that will return the specified HTTP status code
        /// along with a response object serialized as JSON. Automatically adds an appropriate
        /// Content-Type header to the response.
        /// </summary>
        /// <typeparam name="T">The type of the response object being serialized</typeparam>
        /// <param name="statusCode">The status code to be returned</param>
        /// <param name="responseObject">The response object of type T that will be returned as JSON</param>
        /// <returns>The stub that the response was added to</returns>
        public HttpStub ReturnsJson<T>(HttpStatusCode statusCode, T responseObject)
        {
            return Returns(statusCode, new Dictionary<string, object> {{"Content-Type", "application/json"}}, responseObject);
        }

        /// <summary>
        /// Adds a response to the stub that will return the specified HTTP status code
        /// along with a response object serialized as XML. Automatically adds an appropriate
        /// Content-Type header to the response.
        /// </summary>
        /// <typeparam name="T">The type of the response object being serialized</typeparam>
        /// <param name="statusCode">The status code to be returned</param>
        /// <param name="responseObject">The response object of type T that will be returned as XML</param>
        /// <returns>The stub that the response was added to</returns>
        public HttpStub ReturnsXml<T>(HttpStatusCode statusCode, T responseObject)
        {
            return ReturnsXml(statusCode, responseObject, Encoding.UTF8);
        }

        /// <summary>
        /// Adds a response to the stub that will return the specified HTTP status code
        /// along with a response object serialized as XML. Automatically adds an appropriate 
        /// Content-Type header to the response. Serializes the XML with the specified encoding.
        /// </summary>
        /// <typeparam name="T">The type of the response object being serialized</typeparam>
        /// <param name="statusCode">The status code to be returned</param>
        /// <param name="responseObject">The response object of type T that will be returned as XML</param>
        /// <param name="encoding">The encoding with which to serialize the XML</param>
        /// <returns>The stub that the response was added to</returns>
        public HttpStub ReturnsXml<T>(HttpStatusCode statusCode, T responseObject, Encoding encoding)
        {
            var responseObjectXml = ConvertResponseObjectToXml(responseObject, encoding);

            return Returns(statusCode, new Dictionary<string, object> {{"Content-Type", "application/xml"}}, responseObjectXml);
        }

        /// <summary>
        /// Adds a response to the stub that will return the specified HTTP status code
        /// along with a response represented by binary stream.
        /// </summary>
        /// <param name="statusCode">The status code to be returned</param>
        /// <param name="response">Byte array representing binary stream.</param>
        /// <param name="contentType">Content type</param>
        /// <returns>The stub that the response was added to</returns>
        public HttpStub ReturnsBinary(HttpStatusCode statusCode, byte[] response, string contentType)
        {
            var convertedBase64Bytes = Convert.ToBase64String(response);

            return Returns(statusCode, new Dictionary<string, object> {{"Content-Type", contentType}}, convertedBase64Bytes, "binary");
        }

        private static string ConvertResponseObjectToXml<T>(T objectToSerialize, Encoding encoding)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stringWriter = new EncodedStringWriter(encoding);

            var settings = new XmlWriterSettings
            {
                Encoding = encoding
            };

            using (var writer = XmlWriter.Create(stringWriter, settings))
            {
                serializer.Serialize(writer, objectToSerialize);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Adds a response to the stub with the specified content type
        /// </summary>
        /// <param name="statusCode">The status code to be returned</param>
        /// <param name="headers">The headers for the response</param>
        /// <param name="responseObject">The response object that will be returned as the specified content type</param>
        /// <param name="mode">Response mode: text of binary. Text mode is default</param>
        /// <param name="latencyInMilliseconds">The number of milliseconds to be waiting before response will be returned</param>
        /// <returns>The stub that the response was added to</returns>
        public HttpStub Returns(HttpStatusCode statusCode, IDictionary<string, object> headers, object responseObject,
            string mode = "text", int? latencyInMilliseconds = null)
        {
            var fields = new HttpResponseFields
            {
                StatusCode = statusCode,
                ResponseObject = responseObject,
                Headers = headers,
                Mode = mode
            };

            var behavior = latencyInMilliseconds.HasValue
                ? new Behavior
                {
                    LatencyInMilliseconds = latencyInMilliseconds
                }
                : null;

            var response = new IsResponse<HttpResponseFields>(fields, behavior);

            return Returns(response);
        }

        /// <summary>
        /// Adds a proxy to the stub with predicate generators containing specific values
        /// </summary>
        /// <param name="to">endpoint address to proxy to</param>
        /// <param name="proxyMode">proxyalways, proxyonce or proxytransparent</param>
        /// <param name="predicateGenerators">list of predicates that a proxy repsonse will be recorded for</param>
        /// <returns>The stub that the response was added to</returns>
        public HttpStub ReturnsProxy(Uri to, ProxyMode proxyMode,
            IList<MatchesPredicate<HttpPredicateFields>> predicateGenerators)
        {
            var fields = new ProxyResponseFields<HttpPredicateFields>
            {
                To = to,
                Mode = proxyMode,
                PredicateGenerators = predicateGenerators
            };
            
            var response = new ProxyResponse<ProxyResponseFields<HttpPredicateFields>>(fields);

            return Returns(response);
        }

        /// <summary>
        /// Adds a proxy to the stub with predicate generators containing booleans
        /// </summary>
        /// <param name="to">endpoint address to proxy to</param>
        /// <param name="proxyMode">proxyalways, proxyonce or proxytransparent</param>
        /// <param name="predicateGenerators">list of predicates that a proxy repsonse will be recorded for</param>
        /// <returns>The stub that the response was added to</returns>
        public HttpStub ReturnsProxy(Uri to, ProxyMode proxyMode,
            IList<MatchesPredicate<HttpBooleanPredicateFields>> predicateGenerators)
        {
            var fields = new ProxyResponseFields<HttpBooleanPredicateFields>
            {
                To = to,
                Mode = proxyMode,
                PredicateGenerators = predicateGenerators
            };

            var response = new ProxyResponse<ProxyResponseFields<HttpBooleanPredicateFields>>(fields);

            return Returns(response);
        }

        /// <summary>
        /// Adds a response to the stub.
        /// </summary>
        /// <param name="response"
        /// >The response object designating what the stub will return</param>
        /// <returns>The stub that the response was added to</returns>
        public HttpStub Returns(ResponseBase response)
        {
            Responses.Add(response);
            return this;
        }

        /// <summary>
        /// Adds a predicate to the stub that will match when the request path equals the specified path.
        /// </summary>
        /// <param name="path">The path to match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        public HttpStub OnPathEquals(string path)
        {
            var fields = new HttpPredicateFields
            {
                Path = path
            };

            var predicate = new EqualsPredicate<HttpPredicateFields>(fields);

            return On(predicate);
        }

        /// <summary>
        /// Adds a predicate to the stub that will match when the request method equals the specified method.
        /// </summary>
        /// <param name="method">The method to match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        public HttpStub OnMethodEquals(Method method)
        {
            var fields = new HttpPredicateFields
            {
                Method = method
            };

            var predicate = new EqualsPredicate<HttpPredicateFields>(fields);

            return On(predicate);
        }

        /// <summary>
        /// Adds a predicate to the stub that will match when the request path equals the specified path
        /// and the method of the request matches the specified HTTP method.
        /// </summary>
        /// <param name="path">The path to match on</param>
        /// <param name="method">The method to match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        public HttpStub OnPathAndMethodEqual(string path, Method method)
        {
            var fields = new HttpPredicateFields
            {
                Path = path,
                Method = method
            };

            var predicate = new EqualsPredicate<HttpPredicateFields>(fields);

            return On(predicate);
        }

        /// <summary>
        /// Adds a predicate to the stub that allows you to inject JavaScript to determine if the
        /// predicate should match or not.
        /// </summary>
        /// <param name="injectedFunction">JavaScript function that accepts the request object (and
        /// optionally a logger) and returns true or false</param>
        /// <returns>The stub that the predicate was added to</returns>
        public HttpStub OnInjectedFunction(string injectedFunction)
        {
            var predicate = new InjectPredicate(injectedFunction);

            return On(predicate);
        }

        /// <summary>
        /// Adds a predicate to the stub
        /// </summary>
        /// <param name="predicate">The predicate object designating what the stub will match on</param>
        /// <returns>The stub that the predicate was added to</returns>
        public HttpStub On(PredicateBase predicate)
        {
            Predicates.Add(predicate);
            return this;
        }
    }
}
