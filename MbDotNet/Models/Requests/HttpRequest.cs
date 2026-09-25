using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Requests
{
	/// <summary>
	/// A request in the HTTP protocol
	/// </summary>
	public class HttpRequest : Request
	{
		/// <summary>
		/// The path of the request, without the querystring
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("path")]
		public string Path { get; internal set; }

		/// <summary>
		/// The request body
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("body")]
		public string Body { get; internal set; }

		/// <summary>
		/// The request method
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("method")]
		public Method Method { get; internal set; }

		/// <summary>
		/// When the request was made
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("timestamp")]
		public DateTime Timestamp { get; internal set; }

		/// <summary>
		/// The querystring of the request
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("query")]
		public Dictionary<string, object> QueryParameters { get; internal set; }

		/// <summary>
		/// The HTTP headers
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("headers")]
		public Dictionary<string, string> Headers { get; internal set; }
	}
}
