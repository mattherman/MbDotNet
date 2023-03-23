using System.Collections.Generic;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// A "or" predicate
	/// </summary>
	public class OrPredicate : Predicate
	{
		/// <summary>
		/// The predicates that are being combined
		/// </summary>
		[JsonProperty("or")]
		public IEnumerable<Predicate> Predicates { get; private set; }

		/// <summary>
		/// Create a new OrPredicate instance
		/// </summary>
		/// <param name="predicates">The predicates that are being combined</param>
		public OrPredicate(IEnumerable<Predicate> predicates)
		{
			Predicates = predicates;
		}
	}
}
