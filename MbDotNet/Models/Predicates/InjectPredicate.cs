using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public class InjectPredicate : PredicateBase
    {
        [JsonProperty("inject")]
        public string InjectedFunction { get; private set; }

        public InjectPredicate(string injectedFunction)
        {
            InjectedFunction = injectedFunction;
        }
    }
}