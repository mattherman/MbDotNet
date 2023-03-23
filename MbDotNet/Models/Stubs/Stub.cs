using System.Collections.Generic;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Responses;
using Newtonsoft.Json;

namespace MbDotNet.Models.Stubs
{
	/// <summary>
	/// An abstract representation of a stub without a specific protocol
	/// </summary>
	public abstract class Stub
	{
		/// <summary>
		/// A collection of all of the responses set up on this stub.
		/// </summary>
		[JsonProperty("predicates", NullValueHandling = NullValueHandling.Ignore)]
		public ICollection<Predicate> Predicates { get; set; }

		/// <summary>
		/// A collection of all of the predicates set up on this stub.
		/// </summary>
		[JsonProperty("responses", NullValueHandling = NullValueHandling.Ignore)]
		public ICollection<Response> Responses { get; set; }

		/// <summary>
		/// Create a new StubBase instance
		/// </summary>
		protected Stub()
		{
			Responses = new List<Response>();
			Predicates = new List<Predicate>();
		}
	}
}
