using Newtonsoft.Json;
using MbDotNet.Models.Predicates.Fields;

namespace MbDotNet.Models.Predicates
{
    public class StartsWithPredicate<T> : PredicateBase where T : PredicateFields, new()
    {
        [JsonProperty("startsWith")]
        public T Fields { get; private set; }

        public StartsWithPredicate(T fields)
        {
            Fields = fields;
        }

        public StartsWithPredicate(T fields, bool isCaseSensitive, string exceptExpression, XPathSelector selector)
            : base(isCaseSensitive, exceptExpression, selector)
        {
            Fields = fields;
        }
    }
}
