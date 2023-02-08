using Newtonsoft.Json;

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
		[JsonProperty("requestFrom", NullValueHandling = NullValueHandling.Ignore)]
		public bool? RequestFrom { get; set; }

		/// <summary>
		/// The request data
		/// </summary>
		[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
		public bool? Data { get; set; }
	}
}
