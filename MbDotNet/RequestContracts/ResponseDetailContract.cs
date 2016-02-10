using System.Collections.Generic;
using Newtonsoft.Json;

namespace MbDotNet.RequestContracts
{
    [JsonObject]
    internal class ResponseDetailContract
    {
        [JsonProperty("statusCode")]
        private int _statusCode;

        [JsonProperty("headers")]
        private Dictionary<string, string> _headers;

        [JsonProperty("body")]
        private object body;

        public ResponseDetailContract(Response response)
        {
            _statusCode = (int)response.StatusCode;
            body = response.ResponseObject;//JsonConvert.SerializeObject(response.ResponseObject);
            _headers = new Dictionary<string, string>
            {
                {"Content-Type", "application/json"}
            };
        }
    }
}
