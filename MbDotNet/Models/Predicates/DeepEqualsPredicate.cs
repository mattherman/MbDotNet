using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbDotNet.Models.Predicates.Fields;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public class DeepEqualsPredicate<T> : PredicateBase where T : PredicateFields, new()
    {
        [JsonProperty("deepEquals")]
        public T Fields { get; private set; }

        public DeepEqualsPredicate(T fields)
        {
            Fields = fields;
        }

        public DeepEqualsPredicate(T fields, bool isCaseSensitive, string exceptExpression, XPathSelector selector)
            : base(isCaseSensitive, exceptExpression, selector)
        {
            Fields = fields;
        }
    }
}
