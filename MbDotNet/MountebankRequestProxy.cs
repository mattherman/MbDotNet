using System;
using System.Net;
using System.Net.Http;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Newtonsoft.Json;
using MbDotNet.Models.Responses;
using System.Threading.Tasks;

namespace MbDotNet
{
    internal class MountebankRequestProxy : IRequestProxy
    {
        private const string DefaultMountebankUrl = "http://127.0.0.1:2525";
        private const string ImpostersResource = "imposters";
        private readonly IHttpClientWrapper _httpClient;

        public MountebankRequestProxy() : this(DefaultMountebankUrl) { }

        public MountebankRequestProxy(string mountebankUrl) : this(new HttpClientWrapper(new Uri(mountebankUrl))) { }

        /// <summary>
        /// Internal constructor that allows injection of a client for
        /// testing purposes.
        /// </summary>
        /// <param name="httpClient">An injected client</param>
        internal MountebankRequestProxy(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteAllImpostersAsync()
        {
            using (var response = await ExecuteDeleteAsync(ImpostersResource).ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.OK, "Failed to delete the imposters.").ConfigureAwait(false);
            }
            
        }

        public async Task DeleteImposterAsync(int port)
        {
            using (var response = await ExecuteDeleteAsync($"{ImpostersResource}/{port}").ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.OK, $"Failed to delete the imposter with port {port}.").ConfigureAwait(false);
            }
        }

        public async Task CreateImposterAsync(Imposter imposter)
        {
            var json = JsonConvert.SerializeObject(imposter);

            using (var response = await ExecutePostAsync(ImpostersResource, json).ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.Created,
                    $"Failed to create the imposter with port {imposter.Port} and protocol {imposter.Protocol}.").ConfigureAwait(false);
                await HandleDynamicPort(response, imposter).ConfigureAwait(false);
            }
            
        }

        public Task<RetrievedHttpImposter> GetHttpImposterAsync(int port)
        {
            return GetImposterAsync<RetrievedHttpImposter>(port);
        }

        public Task<RetrievedTcpImposter> GetTcpImposterAsync(int port)
        {
            return GetImposterAsync<RetrievedTcpImposter>(port);
        }

        public Task<RetrievedHttpsImposter> GetHttpsImposterAsync(int port)
        {
            return GetImposterAsync<RetrievedHttpsImposter>(port);
        }

        private async Task<T> GetImposterAsync<T>(int port)
        {
            using (var response = await ExecuteGetAsync($"{ImpostersResource}/{port}").ConfigureAwait(false)) 
            {
                await HandleResponse(response, HttpStatusCode.OK, $"Failed to retrieve imposter with port {port}",
                (message) => new ImposterNotFoundException(message)).ConfigureAwait(false);

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<T>(content);
            }
        }

        private Task<HttpResponseMessage> ExecuteGetAsync(string resource) => _httpClient.GetAsync(resource);

        private Task<HttpResponseMessage> ExecuteDeleteAsync(string resource) => _httpClient.DeleteAsync(resource);

        private Task<HttpResponseMessage> ExecutePostAsync(string resource, string json) => _httpClient.PostAsync(resource, new StringContent(json));

        private async Task HandleResponse(HttpResponseMessage response, HttpStatusCode expectedStatusCode,
            string failureErrorMessage, Func<string, Exception> exceptionFactory = null)
        {
            if (exceptionFactory == null)
                exceptionFactory = (message) => new MountebankException(message);

            if (response.StatusCode != expectedStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var errorMessage = $"{failureErrorMessage}\n\nError Message => \n{content}";
                throw exceptionFactory(errorMessage);
            }
        }

        private async Task HandleDynamicPort(HttpResponseMessage response, Imposter imposter)
        {
            if (imposter.Port == default(int))
            {
                try
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var returnedImposter = JsonConvert.DeserializeObject<CreateImposterResponse>(content);
                    imposter.SetDynamicPort(returnedImposter.Port);
                }
                catch (Exception e)
                {
                    throw new MountebankException($"Unable to retrieve port for imposter with name [{imposter.Name}]", e);
                }

            }
        }
    }
}
