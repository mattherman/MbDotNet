using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
	/// <summary>
	/// The "wait" response behavior
	/// </summary>
	public class WaitBehavior : Behavior
	{
		/// <summary>
		/// The latency to add to the response
		/// </summary>
		[JsonProperty("wait", NullValueHandling = NullValueHandling.Ignore)]
		public int LatencyInMilliseconds { get; set; }

		/// <summary>
		/// Creates a new WaitBehavior instance
		/// </summary>
		/// <param name="latencyInMilliseconds">The latency that should be added to the response</param>
		public WaitBehavior(int latencyInMilliseconds)
		{
			LatencyInMilliseconds = latencyInMilliseconds;
		}
	}
}
