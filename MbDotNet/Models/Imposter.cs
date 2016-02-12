using System.Collections.Generic;
using MbDotNet.Enums;
using MbDotNet.Interfaces;
using Newtonsoft.Json;

namespace MbDotNet
{
    public class Imposter : IImposter
    {
        [JsonProperty("port")]
        public virtual int Port { get; private set; }

        [JsonProperty("protocol")]
        public virtual string Protocol { get; private set; }

        [JsonIgnore]
        public virtual bool PendingSubmission { get; set; }

        [JsonProperty("stubs")]
        public ICollection<IStub> Stubs { get; private set; }

        public Imposter(int port, Protocol protocol)
        {
            Port = port;
            Protocol = protocol.ToString().ToLower();
            PendingSubmission = true;

            Stubs = new List<IStub>();
        }

        public IStub AddStub()
        {
            var stub = new Stub();
            Stubs.Add(stub);
            return stub;
        }
    }
}
