using System.Collections.Generic;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public class HttpImposter : Imposter
    {
        [JsonProperty("stubs")]
        public ICollection<HttpStub> Stubs { get; private set; }

        /// <summary>
        /// Optional default response that imposter sends back if no predicate matches a request
        /// </summary>
        [JsonProperty("defaultResponse", NullValueHandling = NullValueHandling.Ignore)]
        public HttpResponseFields DefaultResponse { get; private set; }

        public HttpImposter(int? port, string name, bool recordRequests = false, HttpResponseFields defaultResponse = null) 
            : base(port, Enums.Protocol.Http, name, recordRequests)
        {
            Stubs = new List<HttpStub>();
            DefaultResponse = defaultResponse;
        }

        public HttpStub AddStub()
        {
            var stub = new HttpStub();
            Stubs.Add(stub);
            return stub;
        }
    }
}