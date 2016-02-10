using System.Collections.Generic;
using System.Net;
using MbDotNet.Enums;
using MbDotNet.Exceptions;
using MbDotNet.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace MbDotNet
{
    public class Imposter : IImposter
    {
        [JsonProperty("port")]
        public virtual int Port { get; private set; }

        [JsonProperty("protocol")]
        public virtual string Protocol { get; private set; }

        [JsonIgnore]
        public virtual bool PendingSubmission { get; private set; }

        [JsonProperty("stubs")]
        public ICollection<Stub> Stubs { get; private set; } 

        private readonly IRestClient _client;

        public Imposter(int port, Protocol protocol) : this(port, protocol, new RestClient(Constants.MountebankUrl)) { }

        public Imposter(int port, Protocol protocol, IRestClient client)
        {
            Port = port;
            Protocol = protocol.ToString().ToLower();
            PendingSubmission = true;

            _client = client;

            Stubs = new List<Stub>();
        }

        public Stub AddStub()
        {
            var stub = new Stub();
            Stubs.Add(stub);
            return stub;
        }

        public void Submit()
        {
            SendCreateImposterRequest(this);
            PendingSubmission = false;
        }

        public void Delete()
        {
            if (!PendingSubmission)
            {
                SendDeleteImposterRequest(Port);
            }
        }

        private void SendCreateImposterRequest(Imposter requestObject)
        {
            var request = new RestRequest("imposters", Method.POST);

            var json = JsonConvert.SerializeObject(requestObject);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new MountebankException(string.Format("Failed to create the imposter: {0}", response.ErrorMessage));
            }
        }

        private void SendDeleteImposterRequest(int port)
        {
            var request = new RestRequest(string.Format("imposters/{0}", port), Method.DELETE);

            var response = _client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new MountebankException(string.Format("Failed to delete the imposter: {0}", response.ErrorMessage));
            }
        }
    }
}
