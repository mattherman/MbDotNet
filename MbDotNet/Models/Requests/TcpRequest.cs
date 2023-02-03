using Newtonsoft.Json;

namespace MbDotNet.Models.Requests
{
	/// <summary>
	/// A request in the TCP protcol
	/// </summary>
	public class TcpRequest : Request
	{
		/// <summary>
		/// The data in the request
		/// </summary>
		[JsonProperty("data")]
		public string Data { get; internal set; }
	}
}
