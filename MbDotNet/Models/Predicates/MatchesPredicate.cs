using Newtonsoft.Json;
using MbDotNet.Models.Predicates.Fields;

namespace MbDotNet.Models.Predicates
{
    public class MatchesPredicate<T> : PredicateBase where T : PredicateFields
    {
        [JsonProperty("matches")]
        public T Fields { get; private set; }

        public MatchesPredicate(T fields, bool isCaseSensitive = false, string exceptExpression = null, 
                                    XPathSelector xpath = null, JsonPathSelector jsonpath = null)
            : base(isCaseSensitive, exceptExpression, xpath, jsonpath)
        {
            Fields = fields;
        }
    }
}
