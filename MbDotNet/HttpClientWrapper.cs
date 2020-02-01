using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MbDotNet
{
    internal class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;

        public HttpClientWrapper(Uri baseAddress)
        {
            _client = new HttpClient
            {
                BaseAddress = baseAddress
            };
        }

        public async Task<HttpResponseMessage> DeleteAsync(string resource, CancellationToken cancellationToken = default)
        {
            return await _client.DeleteAsync(resource, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PostAsync(string resource, HttpContent content, CancellationToken cancellationToken = default)
        {
            return await _client.PostAsync(resource, content, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> GetAsync(string resource, CancellationToken cancellationToken = default)
        {
            return await _client.GetAsync(resource, cancellationToken).ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> PutAsync(string resource, HttpContent content, CancellationToken cancellationToken = default)
        {
            return await _client.PutAsync(resource, content, cancellationToken).ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _client.Dispose();
            }
        }
    }
}
