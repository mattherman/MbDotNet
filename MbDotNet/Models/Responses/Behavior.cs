using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
	/// <summary>
	/// A "behavior" response
	/// </summary>
	public class Behavior
	{
		/// <summary>
		/// The latency to add to the response
		/// </summary>
		[JsonProperty("wait", NullValueHandling = NullValueHandling.Ignore)]
		public int? LatencyInMilliseconds { get; set; }
	}
}
