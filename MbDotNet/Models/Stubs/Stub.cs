using System.Collections.Generic;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Responses;
using System.Text.Json.Serialization;

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
		[JsonPropertyName("predicates")]
		public IList<Predicate> Predicates { get; set; }

		/// <summary>
		/// A collection of all of the predicates set up on this stub.
		/// </summary>
		[JsonPropertyName("responses")]
		public IList<Response> Responses { get; set; }

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
