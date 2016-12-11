using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MbDotNet.Interfaces;

namespace MbDotNet
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;

        public HttpClientWrapper()
        {
            _client = new HttpClient();
        }

        public Uri BaseAddress
        {
            get
            {
                return _client.BaseAddress;
            }
            set
            {
                _client.BaseAddress = value;
            }
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
