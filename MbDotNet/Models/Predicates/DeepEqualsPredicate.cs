using MbDotNet.Models.Predicates.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public class DeepEqualsPredicate<T> : PredicateBase where T : PredicateFields
    {
        [JsonProperty("deepEquals")]
        public T Fields { get; private set; }

        public DeepEqualsPredicate(T fields, bool isCaseSensitive = false, string exceptExpression = null, 
                                    XPathSelector xpath = null, JsonPathSelector jsonpath = null)
            : base(isCaseSensitive, exceptExpression, xpath, jsonpath)
        {
            Fields = fields;
        }
    }
}
