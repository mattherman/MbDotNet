using MbDotNet.Models.Requests;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    /// <summary>
    /// The base class for a retrieved imposter.
    /// </summary>
    /// <typeparam name="T">The request type this imposter contains</typeparam>
    public abstract class RetrievedImposter<T> where T: Request
    {
        /// <summary>
        /// The port the imposter is set up to accept requests on.
        /// </summary>
        [JsonProperty("port")]
        public int Port { get; internal set; }

        /// <summary>
        /// The protocol the imposter is set up to accept requests through.
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; internal set; }

        /// <summary>
        /// Optional name for the imposter, used in the logs.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// The number of requests that have been made to this imposter
        /// </summary>
        [JsonProperty("numberOfRequests")]
        public int NumberOfRequests { get; internal set; }

        /// <summary>
        /// The requests that have been made to this imposter
        /// </summary>
        [JsonProperty("requests")]
        public T[] Requests { get; internal set; }
    }
}