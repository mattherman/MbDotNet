using System.Collections.Generic;
using MbDotNet.Models.Requests;
using MbDotNet.Models.Responses.Fields;
using MbDotNet.Models.Stubs;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Imposters
{
	/// <summary>
	/// The base class for a retrieved imposter.
	/// </summary>
	/// <typeparam name="TRequest">The request type this imposter contains</typeparam>
	/// <typeparam name="TResponseFields">The response fields type this imposter contains</typeparam>
	public abstract class RetrievedImposter<TRequest, TResponseFields>
		where TRequest : Request
		where TResponseFields : ResponseFields, new()
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
		/// Optional name for the imposter, used in the logs.
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("name")]
		public string Name { get; internal set; }

		/// <summary>
		/// The number of requests that have been made to this imposter
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("numberOfRequests")]
		public int NumberOfRequests { get; internal set; }

		/// <summary>
		/// The requests that have been made to this imposter
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("requests")]
		public IReadOnlyList<TRequest> Requests { get; internal set; }

		/// <summary>
		/// A set of behaviors used to generate a response for an imposter
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("stubs")]
		public IReadOnlyList<RetrievedStub<TRequest, TResponseFields>> Stubs { get; internal set; }

		/// <summary>
		/// Optional default response that imposter sends back if no predicate matches a request
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("defaultResponse")]
		public TResponseFields DefaultResponse { get; internal set; }
	}
}
