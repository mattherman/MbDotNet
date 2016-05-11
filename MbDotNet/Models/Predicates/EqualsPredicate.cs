using System.Collections.Generic;
using MbDotNet.Enums;
using Newtonsoft.Json;
using MbDotNet.Models.Predicates.Fields;

namespace MbDotNet.Models.Predicates
{
    public class EqualsPredicate<T> : PredicateBase where T : PredicateFields, new()
    {
        [JsonProperty("equals")]
        public T Fields { get; private set; }

        public EqualsPredicate(T fields)
        {
            Fields = fields;
        }

        public EqualsPredicate(T fields, bool isCaseSensitive, string exceptExpression, XPathSelector selector)
            : base(isCaseSensitive, exceptExpression, selector)
        {
            Fields = fields;
        }
    }
}
