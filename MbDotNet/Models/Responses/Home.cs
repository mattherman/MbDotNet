using System;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Responses
{
	/// <summary>
	/// The base response from the Mountebank server
	/// </summary>
	public class Home
	{
		/// <summary>
		/// Links to various information about the Mountebank server
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("_links")]
		public Link Links { get; internal set; }

	}

	/// <summary>
	/// Set of links to information about the Mountebank server
	/// </summary>
	public class Link
	{
		/// <summary>
		/// Link to the configured imposters
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("imposters")]
		public HrefField Imposters { get; internal set; }

		/// <summary>
		/// Link to the server configuration
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("config")]
		public HrefField Config { get; internal set; }

		/// <summary>
		/// Link to the server logs
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("logs")]
		public HrefField Logs { get; internal set; }
	}

	/// <summary>
	/// An href
	/// </summary>
	public class HrefField
	{
		/// <summary>
		/// An href
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("href")]
		public string Href { get; internal set; }
	}
}

