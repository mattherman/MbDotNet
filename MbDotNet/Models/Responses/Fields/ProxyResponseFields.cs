using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System;
using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using Newtonsoft.Json.Converters;
using MbDotNet.Models.Predicates.Fields;

namespace MbDotNet.Models.Responses.Fields
{
    public class ProxyResponseFields<T> : ResponseFields where T: PredicateFields
    {
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
        public Uri To { get; set; }

        [JsonProperty("mode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProxyMode Mode { get; set; }

        [JsonProperty("predicateGenerators", NullValueHandling = NullValueHandling.Ignore)]
        public IList<MatchesPredicate<T>> PredicateGenerators { get; set; } 
    }
}
