using System.Collections.Generic;
using MbDotNet.Enums;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates.Fields
{
    public class HttpPredicateFields : PredicateFields
    {
        /// <summary>
        /// The path of the request, without the querystring
        /// </summary>
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        /// <summary>
        /// The request body
        /// </summary>
        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestBody { get; set; }

        [JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
        private string RawMethod => Method?.ToString().ToUpper();

        /// <summary>
        /// The request method
        /// </summary>
        [JsonIgnore]
        public Method? Method { get; set; }

        /// <summary>
        /// The HTTP headers
        /// </summary>
        [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// The querystring of the request
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> QueryParameters { get; set; }

        /// <summary>
        /// The client socket, primarily used for logging and debugging
        /// </summary>
        [JsonProperty("requestFrom", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestFrom { get; set; }
    }
}
