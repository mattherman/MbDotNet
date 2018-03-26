using System.Collections.Generic;
using Newtonsoft.Json;

namespace MbDotNet.Models.Predicates
{
    public class JsonPathSelector
    {
        [JsonProperty("selector", NullValueHandling = NullValueHandling.Ignore)]
        public string Selector { get; private set; }

        public JsonPathSelector(string selector)
        {
            Selector = selector;
        }
    }
}
