using Newtonsoft.Json;

namespace MbDotNet.RequestContracts
{
    [JsonObject]
    internal class ResponseDetailContract
    {
        [JsonProperty("statusCode")]
        private int _statusCode;

        public ResponseDetailContract(int statusCode)
        {
            _statusCode = statusCode;
        }
    }
}
