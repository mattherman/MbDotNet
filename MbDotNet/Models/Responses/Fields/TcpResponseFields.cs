using System.Text.Json.Serialization;

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
		[JsonPropertyName("data")]
		public string Data { get; set; }
	}
}
