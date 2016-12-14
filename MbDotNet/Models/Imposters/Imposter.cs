using System.Collections.Generic;
using MbDotNet.Models;
using MbDotNet.Enums;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public abstract class Imposter
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Set as virtual for testing purposes")]
        public Imposter(int port, Protocol protocol, string name)
        {
            Port = port;
            Protocol = protocol.ToString().ToLower();
            Name = name;
        }
    }
}
