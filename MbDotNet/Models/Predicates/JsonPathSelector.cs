using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// A jsonpath selector
	/// </summary>
	public class JsonPathSelector
	{
		/// <summary>
		/// A jsonpath selector
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("selector")]
		public string Selector { get; private set; }

		/// <summary>
		/// Create a new JsonPathSelector instance
		/// </summary>
		/// <param name="selector">A jsonpath selector</param>
		public JsonPathSelector(string selector)
		{
			Selector = selector;
		}
	}
}
