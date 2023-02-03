using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MbDotNet.Models.Requests
{
	/// <summary>
	/// A request in the SMTP protocol
	/// </summary>
	public class SmtpRequest : Request
	{
		/// <summary>
		/// The from address sent in the MAIL command
		/// </summary>
		[JsonProperty("envelopeFrom")]
		public string EnvelopeFrom { get; internal set; }

		/// <summary>
		/// The address sent using the RCPT command
		/// </summary>
		[JsonProperty("envelopeTo")]
		public IList<string> EnvelopeTo { get; internal set; }

		/// <summary>
		/// The sender of the message
		/// </summary>
		[JsonProperty("from")]
		public EmailAddress From { get; internal set; }

		/// <summary>
		/// The recipients of the message
		/// </summary>
		[JsonProperty("to")]
		public IList<EmailAddress> To { get; internal set; }

		/// <summary>
		/// The CC recipients of the message
		/// </summary>
		[JsonProperty("cc")]
		public IList<EmailAddress> Cc { get; internal set; }

		/// <summary>
		/// The BCC recipients of the message
		/// </summary>
		[JsonProperty("bcc")]
		public IList<EmailAddress> Bcc { get; internal set; }

		/// <summary>
		/// The subject of the message
		/// </summary>
		[JsonProperty("subject")]
		public string Subject { get; internal set; }

		/// <summary>
		/// The priority of the message
		/// </summary>
		[JsonProperty("priority")]
		public string Priority { get; internal set; }

		/// <summary>
		/// The in reply to of the message
		/// </summary>
		[JsonProperty("inReplyTo")]
		public IList<EmailAddress> InReplyTo { get; internal set; }

		/// <summary>
		/// The text-only message
		/// </summary>
		[JsonProperty("text")]
		public string Text { get; internal set; }

		/// <summary>
		/// The html message
		/// </summary>
		[JsonProperty("html")]
		public string Html { get; internal set; }

		/// <summary>
		/// The message attachments
		/// </summary>
		[JsonProperty("attachments")]
		public IList<EmailAttachment> Attachments { get; internal set; }
	}
}
