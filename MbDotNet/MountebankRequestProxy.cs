using System.Net;
using MbDotNet.Exceptions;
using MbDotNet.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace MbDotNet
{
    public class MountebankRequestProxy : IRequestProxy
    {
        private readonly IRestClient _client;
        private const string MountebankUrl = "http://matt-laptop:2525"; //"http://127.0.0.1:2525";

        public MountebankRequestProxy() : this(new RestClient(MountebankUrl)) {}
        public MountebankRequestProxy(IRestClient client)
        {
            _client = client;
        }

        public void DeleteAllImposters()
        {
            var request = new RestRequest("imposters", Method.DELETE);

            ExecuteRequestAndCheckStatusCode(request, HttpStatusCode.OK, "Failed to delete the imposters.");
        }

        public void DeleteImposter(int port)
        {
            var request = new RestRequest(string.Format("imposters/{0}", port), Method.DELETE);

            ExecuteRequestAndCheckStatusCode(request, HttpStatusCode.OK, string.Format("Failed to delete the imposter with port {0}.", port));
        }

        public void CreateImposter(IImposter imposter)
        {
            var request = new RestRequest("imposters", Method.POST);

            var json = JsonConvert.SerializeObject(imposter);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            ExecuteRequestAndCheckStatusCode(request, HttpStatusCode.Created, string.Format("Failed to create the imposter with port {0} and protocol {1}.", imposter.Port, imposter.Protocol));
        }

        private void ExecuteRequestAndCheckStatusCode(IRestRequest request, HttpStatusCode expectedStatusCode,
            string failureErrorMessage)
        {
            var response = _client.Execute(request);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new MountebankException(string.Format("{0} --- {1}", failureErrorMessage, response.ErrorMessage));
            }
        }
    }
}
