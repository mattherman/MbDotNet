using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates.Fields
{
    public class TcpPredicateFields : PredicateFields
    {
        /// <summary>
        /// The client socket, primarily used for logging and debugging
        /// </summary>
        [JsonProperty("requestFrom", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestFrom { get; set; }

        /// <summary>
        /// The request data
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public string Data { get; set; }
    }
}
