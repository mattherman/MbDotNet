using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MbDotNet
{
    internal class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;

        public HttpClientWrapper(Uri baseAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string resource)
        {
            return await _client.DeleteAsync(resource);
        }

        public async Task<HttpResponseMessage> PostAsync(string resource, HttpContent content)
        {
            return await _client.PostAsync(resource, content);
        }

        public async Task<HttpResponseMessage> GetAsync(string resource)
        {
            return await _client.GetAsync(resource);
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
