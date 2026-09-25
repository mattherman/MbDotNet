using System.Text.Json.Serialization;

namespace MbDotNet.Models.Predicates.Fields
{
	/// <summary>
	/// Used to specify which HTTP predicate fields should be generated for proxy predicates
	/// </summary>
	public class HttpBooleanPredicateFields : PredicateFields
	{
		/// <summary>
		/// The path of the request, without the querystring
		/// </summary>
		[JsonPropertyName("path")]
		public bool? Path { get; set; }

		/// <summary>
		/// The request body
		/// </summary>
		[JsonPropertyName("body")]
		public bool? RequestBody { get; set; }

		/// <summary>
		/// The request method
		/// </summary>
		[JsonPropertyName("method")]
		public bool? Method { get; set; }

		/// <summary>
		/// The HTTP headers
		/// </summary>
		[JsonPropertyName("headers")]
		public bool? Headers { get; set; }

		/// <summary>
		/// The querystring of the request
		/// </summary>
		[JsonPropertyName("query")]
		public bool? QueryParameters { get; set; }

		/// <summary>
		/// The client socket, primarily used for logging and debugging
		/// </summary>
		[JsonPropertyName("requestFrom")]
		public bool? RequestFrom { get; set; }
	}
}
