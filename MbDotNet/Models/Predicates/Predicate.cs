using System.Text.Json.Serialization;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// An abstract representation of a predicate
	/// </summary>
	public abstract class Predicate
	{
		/// <summary>
		/// Whether or not the predicate should be case sensitive when performing matches
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("caseSensitive")]
		public bool IsCaseSensitive { get; private set; }

		/// <summary>
		/// A regular expression to strip out of the request field before matching
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("except")]
		public string ExceptExpression { get; private set; }

		/// <summary>
		/// A xpath selector to narrow the value being matched
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("xpath")]
		public XPathSelector XPathSelector { get; private set; }

		/// <summary>
		/// A jsonpath selector to narrow the value being matched
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("jsonpath")]
		public JsonPathSelector JsonPathSelector { get; private set; }

		/// <summary>
		/// Create a new Predicate instance
		/// </summary>
		protected Predicate() { }

		/// <summary>
		/// Create a new Predicate instance
		/// </summary>
		/// <param name="isCaseSensitive">Whether or not predicate matching is case sensitive</param>
		/// <param name="exceptExpression">A regular expression for eliminating parts of a predicate value</param>
		/// <param name="xpath">A xpath selector for narrowing the predicate value</param>
		/// <param name="jsonpath">A jsonpath selector for narrowing the predicate value</param>
		protected Predicate(bool isCaseSensitive, string exceptExpression, XPathSelector xpath, JsonPathSelector jsonpath)
		{
			IsCaseSensitive = isCaseSensitive;
			ExceptExpression = exceptExpression;
			XPathSelector = xpath;
			JsonPathSelector = jsonpath;
		}
	}
}
