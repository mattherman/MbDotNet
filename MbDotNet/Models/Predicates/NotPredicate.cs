using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// A "not" predicate
	/// </summary>
	public class NotPredicate : Predicate
	{
		/// <summary>
		/// The predicate that is being negated
		/// </summary>
		[JsonProperty("not")]
		public Predicate ChildPredicate { get; private set; }

		/// <summary>
		/// Create a new NotPredicate instance
		/// </summary>
		/// <param name="childPredicate">The predicate that is being negated</param>
		public NotPredicate(Predicate childPredicate)
		{
			ChildPredicate = childPredicate;
		}
	}
}
