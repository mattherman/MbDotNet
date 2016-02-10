using System.Collections.Generic;
using System.Linq;
using System.Net;
using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet
{
    public class Stub
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

        public Stub Returns(HttpStatusCode statusCode)
        {
            Returns(statusCode, null);

            return this;
        }

        public Stub Returns(HttpStatusCode statusCode, object responseObject)
        {
            var responseDetail = new IsResponseDetail(statusCode, responseObject);
            var response = new IsResponse(responseDetail);
            Responses.Add(response);

            return this;
        }
    }
}
