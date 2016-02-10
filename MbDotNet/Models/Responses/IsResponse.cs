using System.Net;
using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet
{
    public class IsResponse : IResponse
    {
        [JsonProperty("is")]
        public IsResponseDetail Detail { get; private set; }

        public IsResponse(IsResponseDetail detail)
        {
            Detail = detail;
        }
    }
}
