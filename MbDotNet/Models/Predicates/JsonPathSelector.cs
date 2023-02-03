using System.Collections.Generic;
using Newtonsoft.Json;

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
		[JsonProperty("selector", NullValueHandling = NullValueHandling.Ignore)]
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
