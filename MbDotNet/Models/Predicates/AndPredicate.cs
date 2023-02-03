using System.Collections.Generic;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// A "and" predicate
	/// </summary>
	public class AndPredicate : PredicateBase
	{
		/// <summary>
		/// The predicates that are being combined
		/// </summary>
		[JsonProperty("and")]
		public IEnumerable<PredicateBase> Predicates { get; private set; }

		/// <summary>
		/// Create a new AndPredicate instance
		/// </summary>
		/// <param name="predicates">The predicates that are being combined</param>
		public AndPredicate(IEnumerable<PredicateBase> predicates)
		{
			Predicates = predicates;
		}
	}
}
