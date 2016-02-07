using System.Collections.Generic;
using Newtonsoft.Json;

namespace MbDotNet.RequestContracts
{
    [JsonObject("stub")]
    internal class StubContract
    {
        [JsonProperty("responses")]
        private ICollection<ResponseContract> _responses;

        public StubContract(ICollection<Response> responses)
        {
            _responses = new List<ResponseContract>();
            foreach (var response in responses)
            {
                this._responses.Add(new ResponseContract(response));
            }
        }
    }
}
