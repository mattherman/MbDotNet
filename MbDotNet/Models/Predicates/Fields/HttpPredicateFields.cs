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
        public object RequestBody { get; set; }

        /// <summary>
        /// Form-encoded key-value pairs in the body. 
        /// Supports key-specific predicates. 
        /// For example, with a body of "firstname=bob&lastname=smith", you
        /// could set a predicate on just "lastname".
        /// </summary>
        [JsonProperty("form", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> FormContent { get; set; }

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
        public IDictionary<string, object> Headers { get; set; }

        /// <summary>
        /// The querystring of the request
        /// </summary>
        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, object> QueryParameters { get; set; }

        /// <summary>
        /// The client socket, primarily used for logging and debugging
        /// </summary>
        [JsonProperty("requestFrom", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestFrom { get; set; }
    }
}
