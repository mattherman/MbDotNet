using System.Text.Json.Serialization;

namespace MbDotNet.Models.Predicates.Fields
{
	/// <summary>
	/// Predicate fields for predicates on TCP imposters
	/// </summary>
	public class TcpPredicateFields : PredicateFields
	{
		/// <summary>
		/// The client socket, primarily used for logging and debugging
		/// </summary>
		[JsonPropertyName("requestFrom")]
		public string RequestFrom { get; set; }

		/// <summary>
		/// The request data
		/// </summary>
		[JsonPropertyName("data")]
		public string Data { get; set; }
	}
}
