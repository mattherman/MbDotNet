using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Predicates.Fields
{
	/// <summary>
	/// Predicate fields for predicates on HTTP imposters
	/// </summary>
	public class HttpPredicateFields : PredicateFields
	{
		/// <summary>
		/// The path of the request, without the querystring
		/// </summary>
		[JsonPropertyName("path")]
		public string Path { get; set; }

		/// <summary>
		/// The request body
		/// </summary>
		[JsonPropertyName("body")]
		[JsonConverter(typeof(JsonHelper.ConsumerPayloadConverter))]
		public object RequestBody { get; set; }

		/// <summary>
		/// Form-encoded key-value pairs in the body.
		/// Supports key-specific predicates.
		/// </summary>
		[JsonPropertyName("form")]
		public Dictionary<string, string> FormContent { get; set; }

		[JsonInclude]
		[JsonPropertyName("method")]
		private string RawMethod => Method?.ToString().ToUpper();

		/// <summary>
		/// The request method
		/// </summary>
		[JsonIgnore]
		public Method? Method { get; set; }

		/// <summary>
		/// The HTTP headers
		/// </summary>
		[JsonPropertyName("headers")]
		public IDictionary<string, object> Headers { get; set; }

		/// <summary>
		/// The querystring of the request
		/// </summary>
		[JsonPropertyName("query")]
		public IDictionary<string, object> QueryParameters { get; set; }

		/// <summary>
		/// The client socket, primarily used for logging and debugging
		/// </summary>
		[JsonPropertyName("requestFrom")]
		public string RequestFrom { get; set; }
	}
}
