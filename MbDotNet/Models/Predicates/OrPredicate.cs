using System.Collections.Generic;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// A "or" predicate
	/// </summary>
	public class OrPredicate : PredicateBase
	{
		/// <summary>
		/// The predicates that are being combined
		/// </summary>
		[JsonProperty("or")]
		public IEnumerable<PredicateBase> Predicates { get; private set; }

		/// <summary>
		/// Create a new OrPredicate instance
		/// </summary>
		/// <param name="predicates">The predicates that are being combined</param>
		public OrPredicate(IEnumerable<PredicateBase> predicates)
		{
			Predicates = predicates;
		}
	}
}
