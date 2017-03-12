using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MbDotNet
{
    public interface IHttpClientWrapper : IDisposable
    {
        Uri BaseAddress { get; set; }

        Task<HttpResponseMessage> DeleteAsync(string resource);
        Task<HttpResponseMessage> PostAsync(string resource, HttpContent content);
        Task<HttpResponseMessage> GetAsync(string resource);
    }
}
