using System;
using System.Net;
using FluentAssertions;
using MbDotNet.Models.Imposters;
using Method = MbDotNet.Enums.Method;
using System.Net.Http;
using System.Text;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanVerifyCallsOnImposter : AcceptanceTest
    {
        private HttpImposter _imposter;
        private RetrievedHttpImposter _retrievedImposter;
        private const int ImposterPort = 6000;
        
        public override void Run()
        {
            DeleteAllImposters();
            CreateImposter();
            CallImposter();
            VerifyImposterWasCalled();
        }

        private void VerifyImposterWasCalled()
        {
            _retrievedImposter = _client.GetHttpImposter(ImposterPort);

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

        private static void CallImposter()
        {
            using(var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:6000/customers?id=123")
                {
                    Content = new StringContent("<TestData>\r\n  <Name>Bob</Name>\r\n  <Email>bob@zmail.com</Email>\r\n</TestData>", Encoding.UTF8, "text/xml")
                };

                var task = httpClient.SendAsync(request);

                task.Wait();

                var response = task.Result;
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }

        private void CreateImposter()
        {
            _imposter = _client.CreateHttpImposter(ImposterPort);
            _client.Submit(_imposter);
        }

        private void DeleteAllImposters()
        {
            _client.DeleteAllImposters();
        }
    }
}
