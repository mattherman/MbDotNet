using Newtonsoft.Json;
using System.Collections.Generic;

namespace MbDotNet.Models.Predicates
{
    public class AndPredicate : PredicateBase 
    {
        [JsonProperty("and")]
        public IEnumerable<PredicateBase> Predicates { get; private set; }

        public AndPredicate(IEnumerable<PredicateBase> predicates)
        {
            Predicates = predicates;
        }
    }
}
