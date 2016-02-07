using System.Collections.Generic;
using System.Net;
using MbDotNet.Enums;
using MbDotNet.Exceptions;
using MbDotNet.Interfaces;
using MbDotNet.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace MbDotNet
{
    public class Imposter : IImposter
    {
        public virtual int Port { get; private set; }
        public virtual Protocol Protocol { get; private set; }
        public virtual bool PendingSubmission { get; private set; }
        public ICollection<Response> Responses { get; private set; }

        private readonly IRestClient _client;

        public Imposter(int port, Protocol protocol) : this(port, protocol, new RestClient(Constants.MountebankUrl)) { }

        public Imposter(int port, Protocol protocol, IRestClient client)
        {
            Port = port;
            Protocol = protocol;
            PendingSubmission = true;

            _client = client;

            Responses = new List<Response>();
        }

        public Imposter Returns(HttpStatusCode statusCode)
        {
            Returns(statusCode, null);

            return this;
        }

        public Imposter Returns(HttpStatusCode statusCode, object responseObject)
        {
            var response = new Response(statusCode, responseObject);
            Responses.Add(response);

            return this;
        }

        public void Submit()
        {
            var request = new ImposterContract(this);
            SendCreateImposterRequest(request);
            PendingSubmission = false;
        }

        public void Delete()
        {
            if (!PendingSubmission)
            {
                SendDeleteImposterRequest(Port);
            }
        }

        private void SendCreateImposterRequest(ImposterContract requestObject)
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
