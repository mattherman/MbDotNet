using System.Collections.Generic;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public class XPathSelector
    {
        [JsonProperty("selector", NullValueHandling = NullValueHandling.Ignore)]
        public string Selector { get; private set; }

        [JsonProperty("ns", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Namespaces { get; private set; }

        public XPathSelector(string selector) : this(selector, null) { }

        public XPathSelector(string selector, Dictionary<string, string> namespaces)
        {
            Selector = selector;
            Namespaces = namespaces;
        }
    }
}
