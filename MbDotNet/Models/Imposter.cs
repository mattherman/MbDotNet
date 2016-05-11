using System.Collections.Generic;
using MbDotNet.Enums;
using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet.Models
{
    public class Imposter<T> : IImposter where T : StubBase, new()
    {
        [JsonProperty("port")]
        public int Port { get; private set; }

        [JsonProperty("protocol")]
        public string Protocol { get; private set; }

        [JsonIgnore]
        public bool PendingSubmission { get; set; }

        [JsonProperty("stubs")]
        public ICollection<T> Stubs { get; private set; }

        public Imposter(int port, Protocol protocol)
        {
            Port = port;
            Protocol = protocol.ToString().ToLower();
            PendingSubmission = true;

            Stubs = new List<T>();
        }

        public T AddStub()
        {
            var stub = new T();
            Stubs.Add(stub);
            return stub;
        }
    }
}
