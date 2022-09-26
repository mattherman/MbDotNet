using System;
using System.Net;
using System.Net.Http;
using MbDotNet.Exceptions;
using MbDotNet.Models.Imposters;
using Newtonsoft.Json;
using MbDotNet.Models.Responses;
using System.Threading.Tasks;
using System.Threading;
using MbDotNet.Models;

using Newtonsoft.Json.Linq;

using System.Collections.Generic;


namespace MbDotNet
{
    internal class MountebankRequestProxy : IRequestProxy
    {
        private const string DefaultMountebankUrl = "http://127.0.0.1:2525";
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

        
        public async Task DeleteAllImpostersAsync(CancellationToken cancellationToken = default)
        {
            using (var response = await _httpClient.DeleteAsync("imposters", cancellationToken).ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.OK, "Failed to delete the imposters.").ConfigureAwait(false);
            }
        }

        public async Task DeleteImposterAsync(int port, CancellationToken cancellationToken = default)
        {
            using (var response = await _httpClient.DeleteAsync($"imposters/{port}", cancellationToken).ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.OK, $"Failed to delete the imposter with port {port}.").ConfigureAwait(false);
            }
        }

        public async Task CreateImposterAsync(Imposter imposter, CancellationToken cancellationToken = default)
        {
            var json = JsonConvert.SerializeObject(imposter);

            using (
                var response = await _httpClient.PostAsync(
                    "imposters",
                    new StringContent(json),
                    cancellationToken
                ).ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.Created,
                    $"Failed to create the imposter with port {imposter.Port} and protocol {imposter.Protocol}.").ConfigureAwait(false);
                await HandleDynamicPort(response, imposter).ConfigureAwait(false);
            }
        }

        public async Task UpdateImposterAsync(Imposter imposter, CancellationToken cancellationToken = default)
        {
            var json = JsonConvert.SerializeObject(imposter);

            using (
                var response = await _httpClient.PutAsync(
                    $"imposters/{imposter.Port}/stubs",
                    new StringContent(json),
                    cancellationToken
                ).ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.OK,
                    $"Failed to replace stubs for the imposter with port {imposter.Port}.",
                    (message) => new ImposterNotFoundException(message)).ConfigureAwait(false);
            }
        }

        
        public async Task<Home> GetEntryHypermediaAsync( CancellationToken cancellationToken = default)
        {
            using (var response = await _httpClient.GetAsync("", cancellationToken).ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.OK, $"Failed to get the entry hypermedia");
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Home>(content);
            }
        }

        public async Task<IEnumerable<Log>> GetLogsAsync(CancellationToken cancellationToken = default)
        {
            using (var response = await _httpClient.GetAsync("logs", cancellationToken).ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.OK, $"Failed to get the logs");
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var logs = JObject.Parse(content)["logs"]?.ToString();
                if (logs == null) throw new Exception("Expected response to include a \"logs\" property");
                return JsonConvert.DeserializeObject<List<Log>>(logs);
            }
        }

        public async Task<IEnumerable<SimpleRetrievedImposter>> GetImpostersAsync(CancellationToken cancellationToken = default)
        {
            using (var response = await _httpClient.GetAsync("imposters", cancellationToken).ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.OK, $"Failed to retrieve the list of imposters");
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var imposters = JObject.Parse(content)["imposters"]?.ToString();
                if (imposters == null) throw new Exception("Expected response to include an \"imposters\" property");
                return JsonConvert.DeserializeObject<List<SimpleRetrievedImposter>>(imposters);
            }
        }

        public async Task<RetrievedHttpImposter> GetHttpImposterAsync(int port, CancellationToken cancellationToken = default)
        {
            return await GetImposterAsync<RetrievedHttpImposter>(port, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<RetrievedTcpImposter> GetTcpImposterAsync(int port, CancellationToken cancellationToken = default)
        {
            return await GetImposterAsync<RetrievedTcpImposter>(port, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<RetrievedHttpsImposter> GetHttpsImposterAsync(int port, CancellationToken cancellationToken = default)
        {
            return await GetImposterAsync<RetrievedHttpsImposter>(port, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<RetrievedSmtpImposter> GetSmtpImposterAsync(int port, CancellationToken cancellationToken = default)
        {
            return await GetImposterAsync<RetrievedSmtpImposter>(port, cancellationToken)
               .ConfigureAwait(false);
        }

        public async Task DeleteSavedRequestsAsync(int port, CancellationToken cancellationToken = default)
        {
            using (var response = await _httpClient.DeleteAsync($"imposters/{port}/savedRequests", cancellationToken).ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.OK, "Failed to delete the imposters saved requests.").ConfigureAwait(false);
            }
        }

        private async Task<T> GetImposterAsync<T>(int port, CancellationToken cancellationToken = default)
        {
            using (var response = await _httpClient.GetAsync($"imposters/{port}", cancellationToken).ConfigureAwait(false)) 
            {
                await HandleResponse(response, HttpStatusCode.OK, $"Failed to retrieve imposter with port {port}",
                (message) => new ImposterNotFoundException(message)).ConfigureAwait(false);

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<T>(content);
            }
        }

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
            if (imposter.Port == default)
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

        public async Task<Config> GetConfigAsync(CancellationToken cancellationToken = default)
        {   
            using (var response = await _httpClient.GetAsync("config", cancellationToken).ConfigureAwait(false))
            {
                await HandleResponse(response, HttpStatusCode.OK, $"Failed to get config").ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Config>(content);
            }
        }
    }
}
