using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using MbDotNet.Interfaces;
using MbDotNet.Models.Responses;
using Newtonsoft.Json;

namespace MbDotNet.Models
{
    public class Stub : IStub
    {
        [JsonProperty("responses")]
        public ICollection<IResponse> Responses { get; private set; }

        [JsonProperty("predicates")]
        public ICollection<IPredicate> Predicates { get; private set; }

        public Stub()
        {
            Responses = new List<IResponse>();
            Predicates = new List<IPredicate>();
        }

        public IStub ReturnsStatus(HttpStatusCode statusCode)
        {
            var response = new IsResponse(statusCode, null, null);
            return Returns(response);
        }

        public IStub ReturnsJson<T>(HttpStatusCode statusCode, T responseObject)
        {
            var headers = new Dictionary<string, string>
            {
                {"Content-Type", "application/json"}
            };

            var response = new IsResponse(statusCode, responseObject, headers);
            return Returns(response);
        }

        public IStub ReturnsXml<T>(HttpStatusCode statusCode, T responseObject)
        {
            var headers = new Dictionary<string, string>
            {
                {"Content-Type", "application/xml"}
            };

            var responseObjectXml = ConvertResponseObjectToXml(responseObject);

            var response = new IsResponse(statusCode, responseObjectXml, headers);
            return Returns(response);
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

        public IStub Returns(IResponse response)
        {
            Responses.Add(response);
            return this;
        }
    }
}
