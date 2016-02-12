using System.Collections.Generic;
using System.Net;
using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet
{
    public class IsResponse : IResponse
    {
        [JsonProperty("is")] 
        private readonly IsResponseDetail _detail;

        public HttpStatusCode StatusCode { get { return _detail.StatusCode; } }
        public object ResponseObject { get { return _detail.ResponseObject; } }
        public ICollection<KeyValuePair<string, string>> Headers { get { return _detail.Headers; } } 

        public IsResponse(HttpStatusCode statusCode, object responseObject, ICollection<KeyValuePair<string, string>> headers)
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
            [JsonProperty("statusCode")]
            public HttpStatusCode StatusCode { get; set; }

            [JsonProperty("body")]
            public object ResponseObject { get; set; }

            [JsonProperty("headers")]
            public ICollection<KeyValuePair<string, string>> Headers { get; set; } 
        }
    }
}
