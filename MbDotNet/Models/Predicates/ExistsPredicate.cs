using MbDotNet.Models.Predicates.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public class ExistsPredicate<T> : PredicateBase where T : PredicateFields
    {
        [JsonProperty("exists")]
        public T Fields { get; private set; }

        public ExistsPredicate(T fields, bool isCaseSensitive = false, string exceptExpression = null,
                                    XPathSelector xpath = null, JsonPathSelector jsonpath = null)
            : base(isCaseSensitive, exceptExpression, xpath, jsonpath)
        {
            Fields = fields;
        }
    }
}