using System;

using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public class RetrievedImposters
    {
        [JsonProperty("port")]
        public int Port { get; internal set; }

        [JsonProperty("protocol")]
        public string Protocol { get; internal set; }

        [JsonProperty("numberOfRequests")]
        public int NumberOfRequests { get; internal set; }


    }
}

