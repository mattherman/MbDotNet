using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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
		[JsonInclude]
		[JsonPropertyName("envelopeFrom")]
		public string EnvelopeFrom { get; internal set; }

		/// <summary>
		/// The address sent using the RCPT command
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("envelopeTo")]
		public IReadOnlyList<string> EnvelopeTo { get; internal set; }

		/// <summary>
		/// The sender of the message
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("from")]
		public EmailAddress From { get; internal set; }

		/// <summary>
		/// The recipients of the message
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("to")]
		public IReadOnlyList<EmailAddress> To { get; internal set; }

		/// <summary>
		/// The CC recipients of the message
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("cc")]
		public IReadOnlyList<EmailAddress> Cc { get; internal set; }

		/// <summary>
		/// The BCC recipients of the message
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("bcc")]
		public IReadOnlyList<EmailAddress> Bcc { get; internal set; }

		/// <summary>
		/// The subject of the message
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("subject")]
		public string Subject { get; internal set; }

		/// <summary>
		/// The priority of the message
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("priority")]
		public string Priority { get; internal set; }

		/// <summary>
		/// The in reply to of the message
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("inReplyTo")]
		public IReadOnlyList<EmailAddress> InReplyTo { get; internal set; }

		/// <summary>
		/// The text-only message
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("text")]
		public string Text { get; internal set; }

		/// <summary>
		/// The html message
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("html")]
		public string Html { get; internal set; }

		/// <summary>
		/// The message attachments
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("attachments")]
		public IReadOnlyList<EmailAttachment> Attachments { get; internal set; }
	}
}
