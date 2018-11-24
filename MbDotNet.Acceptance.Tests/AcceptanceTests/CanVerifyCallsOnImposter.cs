using System;
using System.Net;
using FluentAssertions;
using MbDotNet.Models.Imposters;
using Method = MbDotNet.Enums.Method;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanVerifyCallsOnImposter : AcceptanceTest
    {
        private HttpImposter _imposter;
        private RetrievedHttpImposter _retrievedImposter;
        private const int ImposterPort = 6000;
        
        public override async Task Run()
        {
            await DeleteAllImposters().ConfigureAwait(false);
            await CreateImposter().ConfigureAwait(false);
            await CallImposter().ConfigureAwait(false);
            await VerifyImposterWasCalled().ConfigureAwait(false);
        }

        private async Task VerifyImposterWasCalled()
        {
            _retrievedImposter = await _client.GetHttpImposterAsync(ImposterPort).ConfigureAwait(false);

            _retrievedImposter.NumberOfRequests.Should().Be(1);
            
            // For the request field to be populated, mountebank must be run with the --mock parameter
            // http://www.mbtest.org/docs/api/overview#get-imposter
            var receivedRequest = _retrievedImposter.Requests[0];

            receivedRequest.Path.Should().Be("/customers");
            receivedRequest.QueryParameters["id"].Should().Be("123");
            receivedRequest.Body.Should()
                .Be("<TestData>\r\n  <Name>Bob</Name>\r\n  <Email>bob@zmail.com</Email>\r\n</TestData>");
            receivedRequest.Method.Should().Be(Method.Post);
            receivedRequest.Timestamp.Should().NotBe(default(DateTime));
            receivedRequest.RequestFrom.Should().NotBe(string.Empty);
            receivedRequest.Headers["Content-Type"].Should().Be("text/xml; charset=utf-8");
            receivedRequest.Headers["Content-Length"].Should().Be("75");
        }

        private static async Task CallImposter()
        {
            using(var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:6000/customers?id=123")
                {
                    Content = new StringContent("<TestData>\r\n  <Name>Bob</Name>\r\n  <Email>bob@zmail.com</Email>\r\n</TestData>", Encoding.UTF8, "text/xml")
                };

                var response = await httpClient.SendAsync(request).ConfigureAwait(false);

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        private async Task CreateImposter()
        {
            _imposter = _client.CreateHttpImposter(ImposterPort);
            await _client.SubmitAsync(_imposter).ConfigureAwait(false);
        }

        private async Task DeleteAllImposters()
        {
            await _client.DeleteAllImpostersAsync().ConfigureAwait(false);
        }
    }
}
