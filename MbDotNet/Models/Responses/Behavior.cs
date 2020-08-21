using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
    public class Behavior
    {
        [JsonProperty("wait", NullValueHandling = NullValueHandling.Ignore)]
        public int? LatencyInMilliseconds { get; set; }
    }
}