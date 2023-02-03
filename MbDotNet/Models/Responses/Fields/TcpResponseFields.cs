using Newtonsoft.Json;

namespace MbDotNet.Models.Responses.Fields
{
	/// <summary>
	/// Response fields that can be set for TCP requests
	/// </summary>
	public class TcpResponseFields : ResponseFields
	{
		/// <summary>
		/// The response data
		/// </summary>
		[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
		public string Data { get; set; }
	}
}
