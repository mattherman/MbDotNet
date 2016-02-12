using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
    public class ProxyResponse
    {
        [JsonProperty("proxy")] 
        private ProxyResponseDetail _detail;

        public ProxyResponse()
        {
            _detail = new ProxyResponseDetail();
        }

        private class ProxyResponseDetail
        {
            // Not yet implemented
        }
    }
}
