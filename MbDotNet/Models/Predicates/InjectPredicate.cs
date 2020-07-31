using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public class InjectPredicate : PredicateBase
    {
        [JsonProperty("inject")]
        public string JavaScriptFunction { get; private set; }

        public InjectPredicate(string javaScriptFunction)
        {
            JavaScriptFunction = javaScriptFunction;
        }
    }
}