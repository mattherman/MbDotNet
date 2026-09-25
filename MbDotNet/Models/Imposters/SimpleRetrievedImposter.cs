using System.Text.Json.Serialization;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// A minimal version of the RetrievedImposter model that is
	/// used when requesting the full collection of imposters.
	/// </summary>
	public class SimpleRetrievedImposter
	{
		/// <summary>
		/// The port the imposter is set up to accept requests on.
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("port")]
		public int Port { get; internal set; }

		/// <summary>
		/// The protocol the imposter is set up to accept requests through.
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("protocol")]
		public string Protocol { get; internal set; }

		/// <summary>
		/// The number of requests that have been made to this imposter
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("numberOfRequests")]
		public int NumberOfRequests { get; internal set; }
	}
}
