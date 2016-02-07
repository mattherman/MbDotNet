using Newtonsoft.Json;

namespace MbDotNet.RequestContracts
{
    [JsonObject("response")]
    internal class ResponseContract
    {
        [JsonProperty("is")]
        private ResponseDetailContract _responseDetail;

        [JsonProperty("body")]
        private object body;

        public ResponseContract(Response response)
        {
            body = response.ResponseObject;
            _responseDetail = new ResponseDetailContract((int)response.StatusCode);
        }
    }
}
