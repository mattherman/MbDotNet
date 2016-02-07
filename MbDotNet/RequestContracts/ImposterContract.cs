using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using MbDotNet.Interfaces;
using MbDotNet.RequestContracts;

namespace MbDotNet.RequestModels
{
    [JsonObject("imposter")]
    internal class ImposterContract
    {
        [JsonProperty("port")]
        private int _port;

        [JsonProperty("protocol")]
        private string _protocol;

        [JsonProperty("stubs")]
        private ICollection<StubContract> _stubs;

        public ImposterContract(IImposter imposter)
        {
            _port = imposter.Port;
            _protocol = imposter.Protocol.ToString().ToLower();

            if (imposter.Responses.Any())
            {
                _stubs = new List<StubContract> {new StubContract(imposter.Responses)};
            }
        }
    }
}
