using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task DeleteAllImpostersAsync()
        {
            var response = await ExecuteDeleteAsync(ImpostersResource);
            HandleResponse(response, HttpStatusCode.OK, "Failed to delete the imposters.");
        }

        public async Task DeleteImposterAsync(int port)
        {
            var response = await ExecuteDeleteAsync(string.Format("{0}/{1}", ImpostersResource, port));
            HandleResponse(response, HttpStatusCode.OK, string.Format("Failed to delete the imposter with port {0}.", port));
        }

        public async Task CreateImposterAsync(Imposter imposter)
        {
            var json = JsonConvert.SerializeObject(imposter);
            var response = await ExecutePostAsync(ImpostersResource, json);
            HandleResponse(response, HttpStatusCode.Created, string.Format("Failed to create the imposter with port {0} and protocol {1}.", imposter.Port, imposter.Protocol));
        }

        private async Task<HttpResponseMessage> ExecuteDeleteAsync(string resource)
        {
            using (var client = GetClient())
            {
                client.BaseAddress = _baseUri;
                return await client.DeleteAsync(resource);
            }
        }

        private async Task<HttpResponseMessage> ExecutePostAsync(string resource, string json)
        {
            using (var client = GetClient())
            {
                client.BaseAddress = _baseUri;
                return await client.PostAsync(resource, new StringContent(json));
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
