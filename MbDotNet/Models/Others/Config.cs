using System.Collections.Generic;
using Newtonsoft.Json;

namespace MbDotNet.Models.Others{
    public class Config{
        [JsonProperty("version")]
        public string Version { get; set; }

         [JsonProperty("options")]
        public Dictionary<string, dynamic> Options { get; set; }

         [JsonProperty("process")]
        public Dictionary<string, dynamic> Process { get; set; }
    }
} 