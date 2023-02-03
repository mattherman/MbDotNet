using System;
using Newtonsoft.Json;

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
		[JsonProperty("_links")]
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
		[JsonProperty("imposters")]
		public HrefField Imposters { get; internal set; }

		/// <summary>
		/// Link to the server configuration
		/// </summary>
		[JsonProperty("config")]
		public HrefField Config { get; internal set; }

		/// <summary>
		/// Link to the server logs
		/// </summary>
		[JsonProperty("logs")]
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
		[JsonProperty("href")]
		public string Href { get; internal set; }
	}
}

