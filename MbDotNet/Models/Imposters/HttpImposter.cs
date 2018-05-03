using System.Collections.Generic;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public class HttpImposter : Imposter 
    {
        [JsonProperty("stubs")]
        public ICollection<HttpStub> Stubs { get; private set; }

        public HttpImposter(int? port, string name, bool recordRequests = false) : base(port, Enums.Protocol.Http, name, recordRequests)
        {
            Stubs = new List<HttpStub>();
        }

        public HttpStub AddStub()
        {
            var stub = new HttpStub();
            Stubs.Add(stub);
            return stub;
        }
    }
}
