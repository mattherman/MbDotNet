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
	public class ProxyResponseFields<T> : ResponseFields where T : PredicateFields
	{
		[JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
		public Uri To { get; set; }

		[JsonProperty("mode", NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(StringEnumConverter))]
		public ProxyMode Mode { get; set; }

		[JsonProperty("predicateGenerators", NullValueHandling = NullValueHandling.Ignore)]
		public IList<MatchesPredicate<T>> PredicateGenerators { get; set; }
	}
}
