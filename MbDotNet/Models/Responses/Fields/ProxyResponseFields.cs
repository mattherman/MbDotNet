using System;
using System.Collections.Generic;
using System.Net;
using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
		[JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
		public Uri To { get; set; }

		/// <summary>
		/// The replay behavior of the proxy
		/// </summary>
		[JsonProperty("mode", NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(StringEnumConverter))]
		public ProxyMode Mode { get; set; }

		/// <summary>
		/// An array of objects that defines how the predicates for new stubs are created
		/// </summary>
		[JsonProperty("predicateGenerators", NullValueHandling = NullValueHandling.Ignore)]
		public IList<MatchesPredicate<T>> PredicateGenerators { get; set; }
	}
}
