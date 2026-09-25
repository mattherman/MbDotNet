using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MbDotNet.Models.Predicates
{
	/// <summary>
	/// A xpath selector
	/// </summary>
	public class XPathSelector
	{
		/// <summary>
		/// A xpath selector
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("selector")]
		public string Selector { get; private set; }

		/// <summary>
		/// The xpath namespace map
		/// </summary>
		[JsonInclude]
		[JsonPropertyName("ns")]
		public IDictionary<string, string> Namespaces { get; private set; }

		/// <summary>
		/// Create a new XPathSelector instance
		/// </summary>
		/// <param name="selector">A xpath selector</param>
		/// <param name="namespaces">The xpath namespace map</param>
		public XPathSelector(string selector, IDictionary<string, string> namespaces = null)
		{
			Selector = selector;
			Namespaces = namespaces;
		}
	}
}
