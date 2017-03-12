using Newtonsoft.Json;

namespace MbDotNet.Models.Requests
{
    public class TcpRequest : Request
    {
        [JsonProperty("data")]
        public string Data { get; internal set; }
    }
}
