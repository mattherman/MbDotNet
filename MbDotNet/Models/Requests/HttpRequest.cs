using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
		[JsonProperty("path")]
		public string Path { get; internal set; }

		/// <summary>
		/// The request body
		/// </summary>
		[JsonProperty("body")]
		public string Body { get; internal set; }

		/// <summary>
		/// The request method
		/// </summary>
		[JsonProperty("method")]
		public Method Method { get; internal set; }

		/// <summary>
		/// When the request was made
		/// </summary>
		[JsonProperty("timestamp")]
		public DateTime Timestamp { get; internal set; }

		/// <summary>
		/// The querystring of the request
		/// </summary>
		[JsonProperty("query")]
		public Dictionary<string, object> QueryParameters { get; internal set; }

		/// <summary>
		/// The HTTP headers
		/// </summary>
		[JsonProperty("headers")]
		public Dictionary<string, string> Headers { get; internal set; }
	}
}
