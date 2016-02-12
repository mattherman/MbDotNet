using System.Net;
using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet
{
    public class IsResponse : IResponse
    {
        [JsonProperty("is")] 
        private IsResponseDetail _detail;

        public IsResponse(HttpStatusCode statusCode) : this(statusCode, null) { }

        public IsResponse(HttpStatusCode statusCode, object responseObject)
        {
            _detail = new IsResponseDetail
            {
                StatusCode = statusCode,
                ResponseObject = responseObject
            };
        }

        private class IsResponseDetail
        {
            [JsonProperty("statusCode")]
            public HttpStatusCode StatusCode { get; set; }

            [JsonProperty("body")]
            public object ResponseObject { get; set; }
        }
    }
}
