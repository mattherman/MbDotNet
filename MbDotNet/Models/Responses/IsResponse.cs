using System.Collections.Generic;
using MbDotNet.Models.Responses.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
	/// <summary>
	/// A "is" response
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class IsResponse<T> : Response where T : ResponseFields, new()
	{
		/// <summary>
		/// Response fields to return when matched
		/// </summary>
		[JsonProperty("is")]
		public T Fields { get; set; }

		/// <summary>
		/// Create a new IsResponse instance
		/// </summary>
		/// <param name="fields">The fields to return when matched</param>
		/// <param name="behaviors">Optional response behaviors</param>
		public IsResponse(T fields, IEnumerable<Behavior> behaviors = null) : base(behaviors)
		{
			Fields = fields;
		}
	}
}
