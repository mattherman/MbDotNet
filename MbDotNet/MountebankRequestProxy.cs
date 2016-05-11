using System.Net;
using MbDotNet.Exceptions;
using MbDotNet.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace MbDotNet
{
    internal class MountebankRequestProxy : IRequestProxy
    {
        private readonly IRestClient _client;
        private const string DefaultMountebankUrl = "http://127.0.0.1:2525";
        private const string ImpostersResource = "imposters";
		
        public MountebankRequestProxy() : this(new RestClient(DefaultMountebankUrl)) {}
		
		public MountebankRequestProxy(string mountebankUrl) : this(new RestClient(mountebankUrl)) { }

		public MountebankRequestProxy(IRestClient client)
        {
            _client = client;
        }

        public void DeleteAllImposters()
        {
            var request = new RestRequest(ImpostersResource, Method.DELETE);

            ExecuteRequestAndCheckStatusCode(request, HttpStatusCode.OK, "Failed to delete the imposters.");
        }

        public void DeleteImposter(int port)
        {
            var request = new RestRequest(string.Format("{0}/{1}", ImpostersResource, port), Method.DELETE);

            ExecuteRequestAndCheckStatusCode(request, HttpStatusCode.OK, string.Format("Failed to delete the imposter with port {0}.", port));
        }

        public void CreateImposter(Imposter imposter)
        {
            var request = new RestRequest(ImpostersResource, Method.POST);

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
                var errorMessage = string.Format("{0}\n\nError Message => \n{1}", failureErrorMessage, response.Content);
                throw new MountebankException(errorMessage);
            }
        }
    }
}
