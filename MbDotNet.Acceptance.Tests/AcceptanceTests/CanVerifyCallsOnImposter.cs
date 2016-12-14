using System;
using System.Net;
using FluentAssertions;
using MbDotNet.Models.Imposters;
using RestSharp;
using Method = MbDotNet.Enums.Method;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanVerifyCallsOnImposter
    {
        private readonly MountebankClient _client;
        private HttpImposter _imposter;
        private RetrievedImposter _retrievedImposter;
        const int ImposterPort = 6000;

       

        public CanVerifyCallsOnImposter(MountebankClient client)
        {
            _client = client;
        }

        public void Run()
        {
            DeleteAllImposters();
            CreateImposter();
            CallImposter();
            VerifyImposterWasCalled();
        }
        
        private void VerifyImposterWasCalled()
        {
            _retrievedImposter = _client.GetImposter(ImposterPort);

            // TODO: Fix NumberOfRequests, see https://github.com/mattherman/MbDotNet/issues/19
            // _retrievedImposter.NumberOfRequests.Should().Be(1);
            var receivedRequest = _retrievedImposter.Requests[0];

            receivedRequest.Path.Should().Be("/customers");
            receivedRequest.QueryParameters["id"].Should().Be("123");
            receivedRequest.Body.Should()
                .Be("<TestData>\r\n  <Name>Bob</Name>\r\n  <Email>bob@zmail.com</Email>\r\n</TestData>");
            receivedRequest.Method.Should().Be(Method.Post);
            receivedRequest.Timestamp.Should().NotBe(default(DateTime));
            receivedRequest.RequestFrom.Should().NotBe(string.Empty);
            receivedRequest.Headers["Content-Type"].Should().Be("text/xml");
            receivedRequest.Headers["Content-Length"].Should().Be("75");
        }

        private void CallImposter()
        {
            var restClient = new RestClient("http://localhost:6000");

            var request = new RestRequest("customers", RestSharp.Method.POST);
            request.AddBody(new TestData("Bob", "bob@zmail.com"));
            request.AddQueryParameter("id", "123");
            var response = restClient.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
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