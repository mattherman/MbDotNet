using System.Net;

namespace MbDotNet
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; private set; }

        public object ResponseObject { get; private set; }

        public Response(HttpStatusCode statusCode) : this(statusCode, null) {}

        public Response(HttpStatusCode statusCode, object responseObject)
        {
            StatusCode = statusCode;
            ResponseObject = responseObject;
        }
    }
}
