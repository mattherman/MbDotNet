using System;
using System.Net;
using System.Net.Http;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Newtonsoft.Json;
using MbDotNet.Models.Responses;

namespace MbDotNet
{
    internal class MountebankRequestProxy : IRequestProxy
    {
        private const string DefaultMountebankUrl = "http://127.0.0.1:2525";
        private const string ImpostersResource = "imposters";
        private readonly Uri _baseUri;
        private readonly IHttpClientWrapper _httpClient;

        public MountebankRequestProxy() : this(DefaultMountebankUrl) { }

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
            var response = ExecuteDelete($"{ImpostersResource}/{port}");
            HandleResponse(response, HttpStatusCode.OK, $"Failed to delete the imposter with port {port}.");
        }

        public void CreateImposter(Imposter imposter)
        {
            var json = JsonConvert.SerializeObject(imposter);
            var response = ExecutePost(ImpostersResource, json);
            HandleResponse(response, HttpStatusCode.Created,
                $"Failed to create the imposter with port {imposter.Port} and protocol {imposter.Protocol}.");
            HandleDynamicPort(response, imposter);
        }

        public RetrievedHttpImposter GetHttpImposter(int port)
        {
            return GetImposter<RetrievedHttpImposter>(port);
        }

        public RetrievedTcpImposter GetTcpImposter(int port)
        {
            return GetImposter<RetrievedTcpImposter>(port);
        }

        public RetrievedHttpsImposter GetHttpsImposter(int port)
        {
            return GetImposter<RetrievedHttpsImposter>(port);
        }

        private T GetImposter<T>(int port)
        {
            var response = ExecuteGet($"{ImpostersResource}/{port}");

            HandleResponse(response, HttpStatusCode.OK, $"Failed to retrieve imposter with port {port}",
                (message) => new ImposterNotFoundException(message));

            var content = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(content);
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

        private void HandleResponse(HttpResponseMessage response, HttpStatusCode expectedStatusCode,
            string failureErrorMessage, Func<string, Exception> exceptionFactory = null)
        {
            if (exceptionFactory == null)
                exceptionFactory = (message) => new MountebankException(message);

            if (response.StatusCode != expectedStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var errorMessage = $"{failureErrorMessage}\n\nError Message => \n{content}";
                throw exceptionFactory(errorMessage);
            }
        }

        private void HandleDynamicPort(HttpResponseMessage response, Imposter imposter)
        {
            if (imposter.Port == default(int))
            {
                try
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var returnedImposter = JsonConvert.DeserializeObject<CreateImposterResponse>(content);
                    imposter.SetDynamicPort(returnedImposter.Port);
                }
                catch (Exception e)
                {
                    throw new MountebankException($"Unable to retrieve port for imposter with name [{imposter.Name}]", e);
                }

            }
        }

        private IHttpClientWrapper GetClient()
        {
            return _httpClient ?? new HttpClientWrapper();
        }
    }
}
