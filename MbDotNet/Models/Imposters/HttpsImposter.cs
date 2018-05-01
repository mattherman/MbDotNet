using MbDotNet.Models.Stubs;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MbDotNet.Models.Imposters
{
    public class HttpsImposter : Imposter 
    {
        [JsonProperty("stubs")]
        public ICollection<HttpStub> Stubs { get; private set; }
        
        [JsonProperty("cert", NullValueHandling = NullValueHandling.Ignore)]
        public string Cert { get; private set; }

        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; private set; }

        [JsonProperty("mutualAuth")]
        public bool MutualAuthRequired { get; private set; }

        public HttpsImposter(int? port, string name, bool recordRequests = false) : this(port, name, null, null, false, recordRequests)
        {
        }

        public HttpsImposter(int? port, string name, string key, string cert, bool mutualAuthRequired, bool recordRequests = false) : base(port, Enums.Protocol.Https, name, recordRequests)
        {
            Cert = cert;
            Key = key;
            MutualAuthRequired = mutualAuthRequired;
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
