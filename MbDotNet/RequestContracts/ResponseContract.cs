using Newtonsoft.Json;

namespace MbDotNet.RequestContracts
{
    [JsonObject("response")]
    internal class ResponseContract
    {
        [JsonProperty("is")]
        private ResponseDetailContract _responseDetail;

        public ResponseContract(Response response)
        {
            _responseDetail = new ResponseDetailContract(response);
        }
    }
}
