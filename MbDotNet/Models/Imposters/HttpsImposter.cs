using MbDotNet.Models.Stubs;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MbDotNet.Models.Imposters
{
    public class HttpsImposter : Imposter 
    {
        [JsonProperty("stubs")]
        public ICollection<HttpStub> Stubs { get; private set; }
        
        // TODO This won't serialize key, but how does a user of this imposter know it's using the self-signed cert?
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; private set; }

        public HttpsImposter(int port, string name) : this(port, name, null)
        {
        }

        public HttpsImposter(int port, string name, string key) : base(port, MbDotNet.Enums.Protocol.Https, name)
        {
            Key = key;
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
