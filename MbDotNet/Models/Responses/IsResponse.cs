using MbDotNet.Models.Responses.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
	/// <summary>
	/// A "is" response
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class IsResponse<T> : ResponseBase where T : ResponseFields, new()
	{
		/// <summary>
		/// Response fields to return when matched
		/// </summary>
		[JsonProperty("is")]
		public T Fields { get; set; }

		/// <summary>
		/// Configured response behaviors
		/// </summary>
		[JsonProperty("_behaviors", NullValueHandling = NullValueHandling.Ignore)]
		public Behavior Behavior { get; set; }

		/// <summary>
		/// Create a new IsResponse instance
		/// </summary>
		/// <param name="fields">The fields to return when matched</param>
		/// <param name="behavior">Optional response behavior</param>
		public IsResponse(T fields, Behavior behavior = null)
		{
			Fields = fields;
			Behavior = behavior;
		}
	}
}
