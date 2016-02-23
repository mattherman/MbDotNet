using System.Collections.Generic;
using System.Net;
using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
    public class IsResponse : IResponse
    {
        [JsonProperty("is")] 
        private readonly IsResponseDetail _detail;

        [JsonIgnore]
        public HttpStatusCode StatusCode { get { return _detail.StatusCode; } }

        [JsonIgnore]
        public object ResponseObject { get { return _detail.ResponseObject; } }

        [JsonIgnore]
        public IDictionary<string, string> Headers { get { return _detail.Headers; } }

        public IsResponse(HttpStatusCode statusCode, object responseObject, IDictionary<string, string> headers)
        {
            _detail = new IsResponseDetail
            {
                StatusCode = statusCode,
                ResponseObject = responseObject,
                Headers = headers
            };
        }

        private class IsResponseDetail
        {
            [JsonProperty("statusCode", NullValueHandling = NullValueHandling.Ignore)]
            public HttpStatusCode StatusCode { get; set; }

            [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
            public object ResponseObject { get; set; }

            [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
            public IDictionary<string, string> Headers { get; set; } 
        }
    }
}
