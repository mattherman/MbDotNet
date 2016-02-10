using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
    public class InjectResponse : IResponse
    {
        [JsonProperty("inject")]
        public InjectResponseDetail Detail { get; private set; }

        public InjectResponse()
        {
            Detail = new InjectResponseDetail();
        }
    }
}
