using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MbDotNet
{
    internal interface IHttpClientWrapper : IDisposable
    {
        Task<HttpResponseMessage> DeleteAsync(string resource, CancellationToken cancellationToken = default);
        Task<HttpResponseMessage> PostAsync(string resource, HttpContent content, CancellationToken cancellationToken = default);
        Task<HttpResponseMessage> GetAsync(string resource, CancellationToken cancellationToken = default);
        Task<HttpResponseMessage> PutAsync(string resource, HttpContent content, CancellationToken cancellationToken = default);
    }
}
