using System.Text.Json.Serialization;

namespace MbDotNet.Models.Requests
{
	/// <summary>
	/// An abstract representation of a request without a specific protocol
	/// </summary>
	public abstract class Request
	{
		/// <summary>
		/// The client socket, primarily used for logging and debugging
		/// </summary>
		[JsonPropertyName("requestFrom")]
		public string RequestFrom { get; set; }
	}
}
