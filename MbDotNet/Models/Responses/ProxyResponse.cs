using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
    public class ProxyResponse
    {
        [JsonProperty("proxy")]
        public ProxyResponseDetail Detail { get; private set; }

        public ProxyResponse()
        {
            Detail = new ProxyResponseDetail();
        }
    }
}
