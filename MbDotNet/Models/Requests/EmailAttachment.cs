using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MbDotNet.Models.Requests
{
	/// <summary>
	/// An email attachment
	/// </summary>
	public class EmailAttachment
	{
		/// <summary>
		/// The type of the attachment
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; internal set; }

		/// <summary>
		/// The content type of the attachment
		/// </summary>
		[JsonProperty("contentType")]
		public string ContentType { get; internal set; }

		/// <summary>
		/// The content of the attachment
		/// </summary>
		[JsonProperty("content")]
		public EmailContent Content { get; internal set; }

		/// <summary>
		/// The content disposition of the attachment
		/// </summary>
		[JsonProperty("contentDisposition")]
		public string ContentDisposition { get; internal set; }

		/// <summary>
		/// The size of the attachment
		/// </summary>
		[JsonProperty("size")]
		public long Size { get; internal set; }
	}
}
