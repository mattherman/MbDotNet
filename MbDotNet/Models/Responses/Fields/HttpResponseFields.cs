using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public IDictionary<string, string> Headers { get; set; } 
    }
}
