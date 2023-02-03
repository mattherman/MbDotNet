using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// A "not" predicate
	/// </summary>
	public class NotPredicate : PredicateBase
	{
		/// <summary>
		/// The predicate that is being negated
		/// </summary>
		[JsonProperty("not")]
		public PredicateBase ChildPredicate { get; private set; }

		/// <summary>
		/// Create a new NotPredicate instance
		/// </summary>
		/// <param name="childPredicate">The predicate that is being negated</param>
		public NotPredicate(PredicateBase childPredicate)
		{
			ChildPredicate = childPredicate;
		}
	}
}
