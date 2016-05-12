using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MbDotNet.Models.Predicates.Fields;

namespace MbDotNet.Models.Predicates
{
    public class EndsWithPredicate<T> : PredicateBase where T : PredicateFields, new()
    {
        [JsonProperty("startsWith")]
        public T Fields { get; private set; }

        public EndsWithPredicate(T fields)
        {
            Fields = fields;
        }

        public EndsWithPredicate(T fields, bool isCaseSensitive, string exceptExpression, XPathSelector selector)
            : base(isCaseSensitive, exceptExpression, selector)
        {
            Fields = fields;
        }
    }
}
