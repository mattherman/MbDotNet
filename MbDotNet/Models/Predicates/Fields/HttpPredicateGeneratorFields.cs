using System.Collections.Generic;
using MbDotNet.Enums;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates.Fields
{
    public class HttpPredicateGeneratorFields : PredicateFields
    {
        /// <summary>
        /// The path of the request, without the querystring
        /// </summary>
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Path { get; set; }

        /// <summary>
        /// The request body
        /// </summary>
        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public bool? RequestBody { get; set; }

        [JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
        private bool? RawMethod => Method;

        /// <summary>
        /// The request method
        /// </summary>
        [JsonIgnore]
        public bool? Method { get; set; }

        /// <summary>
        /// The HTTP headers
        /// </summary>
        [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Headers { get; set; }

        /// <summary>
        /// The querystring of the request
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public bool? QueryParameters { get; set; }

        /// <summary>
        /// The client socket, primarily used for logging and debugging
        /// </summary>
        [JsonProperty("requestFrom", NullValueHandling = NullValueHandling.Ignore)]
        public bool? RequestFrom { get; set; }
    }
}
