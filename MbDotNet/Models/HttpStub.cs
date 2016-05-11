using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using MbDotNet.Enums;
using MbDotNet.Interfaces;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using MbDotNet.Models.Responses;
using MbDotNet.Models.Responses.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models
{
    public class HttpStub : StubBase
    {
        public HttpStub()
        {
            Responses = new List<ResponseBase>();
            Predicates = new List<PredicateBase>();
        }

        public HttpStub ReturnsStatus(HttpStatusCode statusCode)
        {
            var fields = new HttpResponseFields
            {
                StatusCode = statusCode
            };

            var response = new IsResponse<HttpResponseFields>(fields);

            return Returns(response);
        }

        public HttpStub ReturnsJson<T>(HttpStatusCode statusCode, T responseObject)
        {
            return Returns(statusCode, new Dictionary<string, string> { { "Content-Type", "application/json" } }, responseObject);
        }

        public HttpStub ReturnsXml<T>(HttpStatusCode statusCode, T responseObject)
        {
            var responseObjectXml = ConvertResponseObjectToXml(responseObject);

            return Returns(statusCode, new Dictionary<string, string> { {"Content-Type", "application/xml"} }, responseObjectXml);
        }

        private static string ConvertResponseObjectToXml<T>(T objectToSerialize)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stringWriter = new StringWriter();

            using (var writer = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(writer, objectToSerialize);
                return stringWriter.ToString();
            }
        }

        public HttpStub Returns(HttpStatusCode statusCode, IDictionary<string, string> headers, object responseObject)
        {
            var fields = new HttpResponseFields
            {
                StatusCode = statusCode,
                ResponseObject = responseObject,
                Headers = headers
            };

            var response = new IsResponse<HttpResponseFields>(fields);

            return Returns(response);
        }

        public HttpStub Returns(IsResponse<HttpResponseFields> response)
        {
            Responses.Add(response);
            return this;
        }

        public HttpStub OnPathEquals(string path)
        {
            var fields = new HttpPredicateFields
            {
                Path = path
            };

            var predicate = new EqualsPredicate<HttpPredicateFields>(fields);

            return On(predicate);
        }

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

        public HttpStub On(EqualsPredicate<HttpPredicateFields> predicate)
        {
            Predicates.Add(predicate);
            return this;
        }
    }
}
