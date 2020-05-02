using System;
using System.Collections.Generic;
using MbDotNet.Enums;
using Newtonsoft.Json;

namespace MbDotNet.Models.Requests
{
    public class HttpRequest : Request
    {
        [JsonProperty("path")]
        public string Path { get; internal set; }

        [JsonProperty("body")]
        public string Body { get; internal set; }

        [JsonProperty("method")]
        public Method Method { get; internal set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; internal set; }

        [JsonProperty("query")]
        public Dictionary<string, object> QueryParameters { get; internal set; }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; internal set; }
    }
}