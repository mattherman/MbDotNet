using System;
using System.Net;
using System.Net.Http;
using MbDotNet.Exceptions;
using MbDotNet.Interfaces;
using MbDotNet.Models.Imposters;
using Newtonsoft.Json;

namespace MbDotNet
{
    internal class MountebankRequestProxy : IRequestProxy
    {
        private const string DefaultMountebankUrl = "http://127.0.0.1:2525";
        private const string ImpostersResource = "imposters";
        private readonly Uri _baseUri;
        private readonly IHttpClientWrapper _httpClient;
		
        public MountebankRequestProxy() : this(DefaultMountebankUrl) {}
		
		public MountebankRequestProxy(string mountebankUrl) : this(new Uri(mountebankUrl)) { }

		public MountebankRequestProxy(Uri baseAddress)
        {
            _baseUri = baseAddress;
        }

        /// <summary>
        /// Internal constructor that allows injection of a client for
        /// testing purposes.
        /// </summary>
        /// <param name="httpClient">An injected client</param>
        internal MountebankRequestProxy(IHttpClientWrapper httpClient) : this()
        {
            _httpClient = httpClient;
        }

        public void DeleteAllImposters()
        {
            var response = ExecuteDelete(ImpostersResource);
            HandleResponse(response, HttpStatusCode.OK, "Failed to delete the imposters.");
        }

        public void DeleteImposter(int port)
        {
            var response = ExecuteDelete(string.Format("{0}/{1}", ImpostersResource, port));
            HandleResponse(response, HttpStatusCode.OK, string.Format("Failed to delete the imposter with port {0}.", port));
        }

        public void CreateImposter(Imposter imposter)
        {
            var json = JsonConvert.SerializeObject(imposter);
            var response = ExecutePost(ImpostersResource, json);
            HandleResponse(response, HttpStatusCode.Created, string.Format("Failed to create the imposter with port {0} and protocol {1}.", imposter.Port, imposter.Protocol));
        }

        public RetrievedImposter GetImposter(int port)
        {
            var response = ExecuteGet($"{ImpostersResource}/{port}");
            var result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<RetrievedImposter>(result);
        }

        private HttpResponseMessage ExecuteGet(string resource)
        {
            using (var client = GetClient())
            {
                client.BaseAddress = _baseUri;
                return client.GetAsync(resource).Result;
            }
        }

        private HttpResponseMessage ExecuteDelete(string resource)
        {
            using (var client = GetClient())
            {
                client.BaseAddress = _baseUri;
                return client.DeleteAsync(resource).Result;
            }
        }

        private HttpResponseMessage ExecutePost(string resource, string json)
        {
            using (var client = GetClient())
            {
                client.BaseAddress = _baseUri;
                return client.PostAsync(resource, new StringContent(json)).Result;
            }
        }

        private void HandleResponse(HttpResponseMessage response, HttpStatusCode expectedStatusCode, string failureErrorMessage)
        {
            if (response.StatusCode != expectedStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var errorMessage = string.Format("{0}\n\nError Message => \n{1}", failureErrorMessage, content);
                throw new MountebankException(errorMessage);
            }
        }

        private IHttpClientWrapper GetClient()
        {
            return _httpClient ?? new HttpClientWrapper();
        }
    }
}
