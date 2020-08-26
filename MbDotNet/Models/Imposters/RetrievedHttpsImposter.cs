using MbDotNet.Models.Requests;
using MbDotNet.Models.Responses.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public class RetrievedHttpsImposter : RetrievedImposter<HttpRequest, HttpResponseFields>
    {
        [JsonProperty("key")]
        public string Key { get; internal set; }

        [JsonProperty("cert")]
        public string Cert { get; internal set; }

        [JsonProperty("mutualAuth")]
        public bool MutualAuthRequired { get; internal set; }
    }
}
