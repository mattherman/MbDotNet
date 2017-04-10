using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbDotNet.Models.Responses
{
    internal class CreateImposterResponse
    {
        /// <summary>
        /// The port the imposter is set up to accept requests on.
        /// </summary>
        [JsonProperty("port")]
        public int Port { get; internal set; }

    }
}
