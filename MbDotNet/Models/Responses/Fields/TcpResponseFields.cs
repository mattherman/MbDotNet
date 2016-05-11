using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses.Fields
{
    public class TcpResponseFields
    {
        /// <summary>
        /// The response data
        /// </summary>
        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public string Data { get; set; }
    }
}
