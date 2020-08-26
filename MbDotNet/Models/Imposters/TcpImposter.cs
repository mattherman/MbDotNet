using System.Collections.Generic;
using MbDotNet.Models.Stubs;
using MbDotNet.Enums;
using MbDotNet.Models.Responses.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public class TcpImposter : Imposter
    {
        [JsonProperty("stubs")]
        public ICollection<TcpStub> Stubs { get; private set; }

        [JsonProperty("mode")]
        public string Mode { get; private set; }

        /// <summary>
        /// Optional default response that imposter sends back if no predicate matches a request
        /// </summary>
        [JsonProperty("defaultResponse", NullValueHandling = NullValueHandling.Ignore)]
        public TcpResponseFields DefaultResponse { get; private set; }

        public TcpImposter(int? port, string name, TcpMode mode, bool recordRequests = false, TcpResponseFields defaultResponse = null) 
            : base(port, Enums.Protocol.Tcp, name, recordRequests)
        {
            Stubs = new List<TcpStub>();
            Mode = mode.ToString().ToLower();
            DefaultResponse = defaultResponse;
        }

        public TcpStub AddStub()
        {
            var stub = new TcpStub();
            Stubs.Add(stub);
            return stub;
        }
    }
}