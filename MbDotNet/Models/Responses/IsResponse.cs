using Newtonsoft.Json;
using MbDotNet.Models.Responses.Fields;

namespace MbDotNet.Models.Responses
{
    public class IsResponse<T> : ResponseBase where T : ResponseFields, new()
    {
        [JsonProperty("is")]
        public T Fields { get; set; }

        [JsonProperty("_behaviors", NullValueHandling = NullValueHandling.Ignore)]
        public Behavior Behavior { get; set; }

        public IsResponse(T fields, Behavior behavior = null)
        {
            Fields = fields;
            Behavior = behavior;
        }
    }
}