using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// A "and" predicate
	/// </summary>
	public class AndPredicate : Predicate
	{
		/// <summary>
		/// The predicates that are being combined
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("and")]
		public IEnumerable<Predicate> Predicates { get; private set; }

		/// <summary>
		/// Create a new AndPredicate instance
		/// </summary>
		/// <param name="predicates">The predicates that are being combined</param>
		public AndPredicate(IEnumerable<Predicate> predicates)
		{
			Predicates = predicates;
		}
	}
}
