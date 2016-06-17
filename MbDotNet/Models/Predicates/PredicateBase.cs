using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public abstract class PredicateBase
    {
        [JsonProperty("caseSensitive", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsCaseSensitive { get; private set; }

        [JsonProperty("except", NullValueHandling = NullValueHandling.Ignore)]
        public string ExceptExpression { get; private set; }

        [JsonProperty("xpath", NullValueHandling = NullValueHandling.Ignore)]
        public XPathSelector Selector { get; private set; }

        public PredicateBase() { }

        public PredicateBase(bool isCaseSensitive, string exceptExpression, XPathSelector selector)
        {
            IsCaseSensitive = isCaseSensitive;
            ExceptExpression = exceptExpression;
            Selector = selector;
        }
    }
}
