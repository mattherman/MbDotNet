using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MbDotNet.Models.Requests
{
    public class SmtpRequest: Request
    {
        [JsonProperty("envelopeFrom")]
        public string EnvelopeFrom { get; internal set; }

        [JsonProperty("envelopeTo")]
        public string EnvelopeTo { get; internal set; }

        [JsonProperty("from")]
        public string From { get; internal set; }

        [JsonProperty("to")]
        public IList<string> To { get; internal set; }

        [JsonProperty("cc")]
        public IList<string> Cc { get; internal set; }

        [JsonProperty("bcc")]
        public IList<string> Bcc { get; internal set; }

        [JsonProperty("subject")]
        public IList<string> Subject { get; internal set; }

        [JsonProperty("priority")]
        public string Priority { get; internal set; }

        [JsonProperty("references")]
        public IList<string> References { get; internal set; }

        [JsonProperty("inReplyTo")]
        public IList<string> InReplyTo { get; internal set; }

        [JsonProperty("text")]
        public string Text { get; internal set; }

        [JsonProperty("html")]
        public string Html { get; internal set; }

        [JsonProperty("attachments")]
        public IList<string> Attachments { get; internal set; }
    }
}
