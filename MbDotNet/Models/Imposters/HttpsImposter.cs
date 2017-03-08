using System.Collections.Generic;
using MbDotNet.Enums;
using MbDotNet.Interfaces;
using MbDotNet.Models.Stubs;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public class HttpsImposter : Imposter 
    {
        [JsonProperty("stubs")]
        public ICollection<HttpStub> Stubs { get; private set; }

        public HttpsImposter(int port, string name) : base(port, MbDotNet.Enums.Protocol.Https, name)
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
