using System;
using System.Collections.Generic;
using MbDotNet.Enums;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public class RetrievedImposter
    {

        /// <summary>
        /// The port the imposter is set up to accept requests on.
        /// </summary>
        [JsonProperty("port")]
        public int Port { get; private set; }

        /// <summary>
        /// The protocol the imposter is set up to accept requests through.
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; private set; }

        /// <summary>
        /// Optional name for the imposter, used in the logs.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("numberOfRequests")]
        public int NumberOfRequests { get; private set; }

        [JsonProperty("requests")]
        public Requests[] Requests { get; private set; }

    }

    public class Requests
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