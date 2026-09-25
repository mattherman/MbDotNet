using System;
using System.Collections.Generic;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Responses.Fields
{
	/// <summary>
	/// Response fields for configuring a "proxy" response
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ProxyResponseFields<T> : ResponseFields where T : PredicateFields
	{
		/// <summary>
		/// The origin server that the request should proxy to
		/// </summary>
		[JsonPropertyName("to")]
		public Uri To { get; set; }

		/// <summary>
		/// The replay behavior of the proxy
		/// </summary>
		[JsonPropertyName("mode")]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ProxyMode Mode { get; set; }

		/// <summary>
		/// An array of objects that defines how the predicates for new stubs are created
		/// </summary>
		[JsonPropertyName("predicateGenerators")]
		public IList<MatchesPredicate<T>> PredicateGenerators { get; set; }
	}
}
