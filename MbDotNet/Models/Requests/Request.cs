using Newtonsoft.Json;

namespace MbDotNet.Models.Requests
{
    public abstract class Request
    {
        [JsonProperty("requestFrom")]
        public string RequestFrom { get; set; }
    }
}
