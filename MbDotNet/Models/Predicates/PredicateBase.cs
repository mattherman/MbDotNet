using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// An abstract representation of a predicate
	/// </summary>
	public abstract class PredicateBase
	{
		/// <summary>
		/// Whether or not the predicate should be case sensitive when performing matches
		/// </summary>
		[JsonProperty("caseSensitive", NullValueHandling = NullValueHandling.Ignore)]
		public bool IsCaseSensitive { get; private set; }

		/// <summary>
		/// A regular expression to strip out of the request field before matching
		/// </summary>
		[JsonProperty("except", NullValueHandling = NullValueHandling.Ignore)]
		public string ExceptExpression { get; private set; }

		/// <summary>
		/// A xpath selector to narrow the value being matched
		/// </summary>
		[JsonProperty("xpath", NullValueHandling = NullValueHandling.Ignore)]
		public XPathSelector XPathSelector { get; private set; }

		/// <summary>
		/// A jsonpath selector to narrow the value being matched
		/// </summary>
		[JsonProperty("jsonpath", NullValueHandling = NullValueHandling.Ignore)]
		public JsonPathSelector JsonPathSelector { get; private set; }

		/// <summary>
		/// Create a new PredicateBase instance
		/// </summary>
		protected PredicateBase() { }

		/// <summary>
		/// Create a new PredicateBase instance
		/// </summary>
		/// <param name="isCaseSensitive">Whether or not predicate matching is case sensitive</param>
		/// <param name="exceptExpression">A regular expression for eliminating parts of a predicate value</param>
		/// <param name="xpath">A xpath selector for narrowing the predicate value</param>
		/// <param name="jsonpath">A jsonpath selector for narrowing the predicate value</param>
		protected PredicateBase(bool isCaseSensitive, string exceptExpression, XPathSelector xpath, JsonPathSelector jsonpath)
		{
			IsCaseSensitive = isCaseSensitive;
			ExceptExpression = exceptExpression;
			XPathSelector = xpath;
			JsonPathSelector = jsonpath;
		}
	}
}
