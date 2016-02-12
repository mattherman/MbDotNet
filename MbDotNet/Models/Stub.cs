using System;
using System.Collections.Generic;
using System.Net;
using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet
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

        public IStub ReturnsJson(HttpStatusCode statusCode, object responseObject)
        {
            var response = new IsResponse(statusCode, responseObject, null);
            return Returns(response);
        }

        public IStub ReturnsXml(HttpStatusCode statusCode, object responseObject)
        {
            throw new NotImplementedException();
        }

        public IStub Returns(IResponse response)
        {
            Responses.Add(response);
            return this;
        }
    }
}
