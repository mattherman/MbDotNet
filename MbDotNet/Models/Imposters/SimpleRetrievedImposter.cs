using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    /// <summary>
    /// A minimal version of the RetrievedImposter model that is
    /// used when requesting the full collection of imposters.
    /// </summary>
    public class SimpleRetrievedImposter
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
        /// The number of requests that have been made to this imposter
        /// </summary>
        [JsonProperty("numberOfRequests")]
        public int NumberOfRequests { get; internal set; }
    }
}