using MbDotNet.Models.Requests;
using MbDotNet.Models.Responses.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// The class for a match of a retrieved stub.
	/// </summary>
	/// <typeparam name="TRequest">The request type this match contains</typeparam>
	/// <typeparam name="TResponseFields">The response fields type this match contains</typeparam>
	public class Match<TRequest, TResponseFields>
		where TRequest : Request
		where TResponseFields : ResponseFields, new()
	{
		/// <summary>
		/// The request that was matched by the stub
		/// </summary>
		[JsonProperty("request")]
		public TRequest Request { get; set; }

		/// <summary>
		/// The response returned when the stub was matched
		/// </summary>
		[JsonProperty("response")]
		public TResponseFields Response { get; set; }

		/// <summary>
		/// When the match occurred
		/// </summary>
		[JsonProperty("timestamp")]
		public string Timestamp { get; set; }
	}
}
