using System;
using MbDotNet.Enums;
using MbDotNet.Models.Requests;
using Newtonsoft.Json;

namespace MbDotNet.Models.Imposters
{
    public class RetrievedTcpImposter : RetrievedImposter<TcpRequest>
    {
        [JsonProperty("mode")]
        internal string RawMode { get; set; }

        public TcpMode Mode => string.Equals(RawMode, "text", StringComparison.CurrentCultureIgnoreCase) ? TcpMode.Text : TcpMode.Binary;
    }
}
