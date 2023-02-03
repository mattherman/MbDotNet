using MbDotNet.Models.Predicates.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// A "matches" predicate
	/// </summary>
	/// <typeparam name="T">The type of predicate fields based on the imposter type the predicate is added to</typeparam>
	public class MatchesPredicate<T> : PredicateBase where T : PredicateFields
	{
		/// <summary>
		/// The predicate fields to match on
		/// </summary>
		[JsonProperty("matches")]
		public T Fields { get; private set; }

		/// <summary>
		/// Create a new ContainsPredicate instance
		/// </summary>
		/// <param name="fields">The predicate fields to match on</param>
		/// <param name="isCaseSensitive">Whether matches should be case sensitive, defaults to false</param>
		/// <param name="exceptExpression">A regular expression to strip out of the request field before matching</param>
		/// <param name="xpath">An optional xpath selector to narrow the value being matched</param>
		/// <param name="jsonpath">An optional jsonpath selector to narrow the value being matched</param>
		public MatchesPredicate(T fields, bool isCaseSensitive = false, string exceptExpression = null,
									XPathSelector xpath = null, JsonPathSelector jsonpath = null)
			: base(isCaseSensitive, exceptExpression, xpath, jsonpath)
		{
			Fields = fields;
		}
	}
}
