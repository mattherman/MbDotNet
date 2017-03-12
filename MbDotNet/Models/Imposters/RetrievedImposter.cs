using MbDotNet.Models.Requests;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public abstract class RetrievedImposter<T> where T: Request
    {
        /// <summary>
        /// The port the imposter is set up to accept requests on.
        /// </summary>
        [JsonProperty("port")]
        public int Port { get; protected set; }

        /// <summary>
        /// The protocol the imposter is set up to accept requests through.
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; protected set; }

        /// <summary>
        /// Optional name for the imposter, used in the logs.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; protected set; }

        /// <summary>
        /// The number of requests that have been made to this imposter
        /// </summary>
        [JsonProperty("numberOfRequests")]
        public int NumberOfRequests { get; protected set; }

        /// <summary>
        /// The requests that have been made to this imposter
        /// </summary>
        [JsonProperty("requests")]
        public abstract T[] Requests { get; protected set; }
    }
}