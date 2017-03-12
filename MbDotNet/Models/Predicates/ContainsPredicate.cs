using Newtonsoft.Json;
using MbDotNet.Models.Predicates.Fields;

namespace MbDotNet.Models.Predicates
{
    public class ContainsPredicate<T> : PredicateBase where T : PredicateFields, new()
    {
        [JsonProperty("contains")]
        public T Fields { get; private set; }

        public ContainsPredicate(T fields)
        {
            Fields = fields;
        }

        public ContainsPredicate(T fields, bool isCaseSensitive, string exceptExpression, XPathSelector selector)
            : base(isCaseSensitive, exceptExpression, selector)
        {
            Fields = fields;
        }
    }
}
