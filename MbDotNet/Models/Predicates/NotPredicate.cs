using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public class NotPredicate : PredicateBase
    {
        [JsonProperty("not")]
        public PredicateBase ChildPredicate { get; private set; }

        public NotPredicate(PredicateBase childPredicate)
        {
            ChildPredicate = childPredicate;
        }
    }
}
