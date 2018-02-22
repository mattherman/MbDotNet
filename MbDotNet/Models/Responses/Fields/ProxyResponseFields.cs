using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using System;
using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using Newtonsoft.Json.Converters;

namespace MbDotNet.Models.Responses.Fields
{
    public class ProxyResponseFields : ResponseFields
    {
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
        public Uri To { get; set; }

        [JsonProperty("mode", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProxyMode Mode { get; set; }

        [JsonProperty("predicateGenerators", NullValueHandling = NullValueHandling.Ignore)]
        public List<PredicateBase> PredicateGenerators { get; set; } 
    }
}
