using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbDotNet.Enums;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates.Fields
{
    public class HttpPredicateFields : PredicateFields
    {
        [JsonProperty("path", NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }

        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public string RequestBody { get; set; }

        [JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
        private string RawMethod { get { return Method.HasValue ? Method.Value.ToString().ToUpper() : null; } }

        public Method? Method { get; set; }

        [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Headers { get; set; }

        [JsonProperty("query", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> QueryParameters { get; set; }
    }
}
