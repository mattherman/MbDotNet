using System.Net;
using Newtonsoft.Json;

namespace MbDotNet
{
    public class IsResponseDetail
    {
        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; private set; }

        [JsonProperty("body")]
        public object ResponseObject { get; private set; }

        public IsResponseDetail(HttpStatusCode statusCode) : this(statusCode, null) {}

        public IsResponseDetail(HttpStatusCode statusCode, object responseObject)
        {
            StatusCode = statusCode;
            ResponseObject = responseObject;
        }
    }
}
