using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using MbDotNet.Enums;
using MbDotNet.Interfaces;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Responses;
using Newtonsoft.Json;

namespace MbDotNet.Models
{
    public class HttpStub : StubBase
    {
        public HttpStub()
        {
            Responses = new List<IResponse>();
            Predicates = new List<IPredicate>();
        }

        public HttpStub ReturnsStatus(HttpStatusCode statusCode)
        {
            var response = new IsResponse(statusCode, null, null);
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
            var response = new IsResponse(statusCode, responseObject, headers);
            return Returns(response);
        }

        public HttpStub Returns(IResponse response)
        {
            Responses.Add(response);
            return this;
        }

        public HttpStub OnPathEquals(string path)
        {
            var predicate = new EqualsPredicate(path, null, null, null, null);
            return On(predicate);
        }

        public HttpStub OnPathAndMethodEqual(string path, Method method)
        {
            var predicate = new EqualsPredicate(path, method, null, null, null);
            return On(predicate);
        }

        public HttpStub On(IPredicate predicate)
        {
            Predicates.Add(predicate);
            return this;
        }
    }
}
