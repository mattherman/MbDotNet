using System.Net;
using FluentAssertions;
using MbDotNet.Models.Imposters;
using RestSharp;
using Method = MbDotNet.Enums.Method;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    public class TestData
    {
        public string Email { get; set; }
    }

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

            _retrievedImposter.NumberOfRequests.Should().Be(1);
            _retrievedImposter.Requests[0].Path.Should().Be("/customers/123");
        }

        private void CallImposter()
        {
            var restClient = new RestClient("http://localhost:6000");

            var request1 = new RestRequest("customers/123", RestSharp.Method.POST);
            var response1 = restClient.Execute<TestData>(request1);

            response1.StatusCode.Should().Be(HttpStatusCode.OK);
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