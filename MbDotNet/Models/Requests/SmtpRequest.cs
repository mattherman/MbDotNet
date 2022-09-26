using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MbDotNet.Models.Requests
{
	public class SmtpRequest : Request
	{
		[JsonProperty("envelopeFrom")]
		public string EnvelopeFrom { get; internal set; }

		[JsonProperty("envelopeTo")]
		public IList<string> EnvelopeTo { get; internal set; }

		[JsonProperty("from")]
		public EmailAddress From { get; internal set; }

		[JsonProperty("to")]
		public IList<EmailAddress> To { get; internal set; }

		[JsonProperty("cc")]
		public IList<EmailAddress> Cc { get; internal set; }

		[JsonProperty("bcc")]
		public IList<EmailAddress> Bcc { get; internal set; }

		[JsonProperty("subject")]
		public string Subject { get; internal set; }

		[JsonProperty("priority")]
		public string Priority { get; internal set; }

		[JsonProperty("inReplyTo")]
		public IList<EmailAddress> InReplyTo { get; internal set; }

		[JsonProperty("text")]
		public string Text { get; internal set; }

		[JsonProperty("html")]
		public string Html { get; internal set; }

		[JsonProperty("attachments")]
		public IList<EmailAttachment> Attachments { get; internal set; }
	}
}
