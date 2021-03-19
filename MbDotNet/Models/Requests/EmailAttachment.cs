using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MbDotNet.Models.Requests
{
    public class EmailAttachment
    {
        [JsonProperty("type")]
        public string Type { get; internal set; }

        [JsonProperty("contentType")]
        public string ContentType { get; internal set; }

        [JsonProperty("content")]
        public EmailContent Content { get; internal set; }

        [JsonProperty("contentDisposition")]
        public string ContentDisposition { get; internal set; }

        [JsonProperty("size")]
        public long Size { get; internal set; }
    }
}
