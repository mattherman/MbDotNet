using System.Collections.Generic;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Responses;
using Newtonsoft.Json;

namespace MbDotNet.Models.Stubs
{
	/// <summary>
	/// An abstract representation of a stub without a specific protocol
	/// </summary>
	public abstract class StubBase
	{
		/// <summary>
		/// A collection of all of the responses set up on this stub.
		/// </summary>
		[JsonProperty("predicates", NullValueHandling = NullValueHandling.Ignore)]
		public ICollection<PredicateBase> Predicates { get; set; }

		/// <summary>
		/// A collection of all of the predicates set up on this stub.
		/// </summary>
		[JsonProperty("responses", NullValueHandling = NullValueHandling.Ignore)]
		public ICollection<Response> Responses { get; set; }

		/// <summary>
		/// Create a new StubBase instance
		/// </summary>
		protected StubBase()
		{
			Responses = new List<Response>();
			Predicates = new List<PredicateBase>();
		}
	}
}
