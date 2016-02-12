using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet.Models.Responses
{
    public class InjectResponse : IResponse
    {
        [JsonProperty("inject")] 
        private InjectResponseDetail _detail;

        public InjectResponse()
        {
            _detail = new InjectResponseDetail();
        }

        private class InjectResponseDetail
        {
            // Not yet implemented
        }
    }
}
