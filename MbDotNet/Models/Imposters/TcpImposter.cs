using System.Collections.Generic;
using MbDotNet.Models.Stubs;
using MbDotNet.Enums;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public class TcpImposter : Imposter
    {
        [JsonProperty("stubs")]
        public ICollection<TcpStub> Stubs { get; private set; }

        [JsonProperty("mode")]
        public string Mode { get; private set; }

        public TcpImposter(int? port, string name, TcpMode mode, bool recordRequests = false) : base(port, Enums.Protocol.Tcp, name, recordRequests)
        {
            Stubs = new List<TcpStub>();
            Mode = mode.ToString().ToLower();
        }

        public TcpStub AddStub()
        {
            var stub = new TcpStub();
            Stubs.Add(stub);
            return stub;
        }
    }
}
