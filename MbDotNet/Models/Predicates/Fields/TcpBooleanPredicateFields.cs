using System.Text.Json.Serialization;

namespace MbDotNet.Models.Predicates.Fields
{
	/// <summary>
	/// Used to specify which TCP predicate fields should be generated for proxy predicates
	/// </summary>
	public class TcpBooleanPredicateFields : PredicateFields
	{
		/// <summary>
		/// The client socket, primarily used for logging and debugging
		/// </summary>
		[JsonPropertyName("requestFrom")]
		public bool? RequestFrom { get; set; }

		/// <summary>
		/// The request data
		/// </summary>
		[JsonPropertyName("data")]
		public bool? Data { get; set; }
	}
}
