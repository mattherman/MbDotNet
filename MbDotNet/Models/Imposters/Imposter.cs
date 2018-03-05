using MbDotNet.Enums;
using MbDotNet.Exceptions;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public abstract class Imposter
    {
        /// <summary>
        /// The port the imposter is set up to accept requests on.
        /// </summary>
        [JsonProperty(PropertyName = "port", DefaultValueHandling = DefaultValueHandling.Ignore)]
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

        internal void SetDynamicPort(int port)
        {
            if (Port != default(int))
            {
                throw new MountebankException("Cannot change imposter port once it has been set.");
            }

            Port = port;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "Set as virtual for testing purposes")]
        public Imposter(int? port, Protocol protocol, string name)
        {
            if (port.HasValue)
            {
                Port = port.Value;
            }
            
            Protocol = protocol.ToString().ToLower();
            Name = name;
        }
    }
}
