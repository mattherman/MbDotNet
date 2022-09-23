using System;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
    public class Log
    {
        [JsonProperty("level")]
        public string Level { get; internal set; }

        [JsonProperty("message")]
        public string Message { get; internal set; }

        [JsonProperty("Timestamp")]
        public DateTime Timestamp { get; internal set; }
    }
}