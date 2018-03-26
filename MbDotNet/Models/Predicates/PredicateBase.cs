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
        public XPathSelector XPathSelector { get; private set; }

        [JsonProperty("jsonpath", NullValueHandling = NullValueHandling.Ignore)]
        public JsonPathSelector JsonPathSelector { get; private set; }

        public PredicateBase() { }

        public PredicateBase(bool isCaseSensitive, string exceptExpression, XPathSelector xpath, JsonPathSelector jsonpath)
        {
            IsCaseSensitive = isCaseSensitive;
            ExceptExpression = exceptExpression;
            XPathSelector = xpath;
            JsonPathSelector = jsonpath;
        }
    }
}
