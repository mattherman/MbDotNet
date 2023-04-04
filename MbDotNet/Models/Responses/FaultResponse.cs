using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MbDotNet.Models.Responses
{
	/// <summary>
	/// A "fault" response
	/// </summary>
	public class FaultResponse : Response
	{
		/// <summary>
		/// The type of fault to return in the response
		/// </summary>
		[JsonProperty("fault")]
		[JsonConverter(typeof(StringEnumConverter))]
		public Fault Fault { get; set; }

		/// <summary>
		/// Create a new FaultResponse instance
		/// </summary>
		/// <param name="fault">The fault to return in the response</param>
		/// <param name="behaviors">Optional response behavior</param>
		public FaultResponse(Fault fault, IEnumerable<Behavior> behaviors = null) : base(behaviors)
		{
			Fault = fault;
		}
	}
}
