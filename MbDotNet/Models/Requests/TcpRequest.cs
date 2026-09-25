using System.Text.Json.Serialization;

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
		[JsonInclude]
		[JsonPropertyName("data")]
		public string Data { get; internal set; }
	}
}
