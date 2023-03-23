using MbDotNet.Models.Responses.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
	/// <summary>
	/// A "proxy" response for record/replay behavior
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ProxyResponse<T> : Response where T : ResponseFields, new()
	{
		/// <summary>
		/// The fields that should be captured for generated predicates
		/// </summary>
		[JsonProperty("proxy")]
		public T Fields { get; set; }

		/// <summary>
		/// Create a new ProxyResponse instance
		/// </summary>
		/// <param name="fields">The fields that should be captured for generated predicates</param>
		public ProxyResponse(T fields)
		{
			Fields = fields;
		}
	}
}
