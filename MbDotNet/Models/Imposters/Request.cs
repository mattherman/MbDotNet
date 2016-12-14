using System;
using System.Collections.Generic;
using MbDotNet.Enums;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public class Request
    {
        [JsonProperty("path")]
        public string Path { get; private set; }

        [JsonProperty("body")]
        public string Body { get; private set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("requestFrom")]
        public string RequestFrom { get; set; }

        [JsonProperty("headers")]
        public Dictionary<string, string> Headers { get; set; }
    }
}