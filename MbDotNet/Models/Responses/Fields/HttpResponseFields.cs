using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;

namespace MbDotNet.Models.Responses.Fields
{
    public class HttpResponseFields : ResponseFields
    {
        [JsonProperty("statusCode", NullValueHandling = NullValueHandling.Ignore)]
        public HttpStatusCode? StatusCode { get; set; }

        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public object ResponseObject { get; set; }

        [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, object> Headers { get; set; }
        
        [JsonProperty("_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string Mode { get; set; } 
    }
}
