using FluentAssertions;
using MbDotNet.Models.Imposters;
using MbDotNet.Enums;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCheckMatchesForImposter : AcceptanceTest
    {
        private const int ImposterPort = 6000;
        private RetrievedHttpImposter _retrievedImposter;

        public override void Run()
        {
            DeleteAllImposters();
            CreateImposter();
            MakeRequestToImposter();
            GetImposter();
            VerifyImposterWasRetrieved();
            DeleteAllImposters();
        }

        private void GetImposter()
        {
            _retrievedImposter = _client.GetHttpImposter(ImposterPort);
        }

        private void DeleteAllImposters()
        {
            _client.DeleteAllImposters();
        }

        private void VerifyImposterWasRetrieved()
        {
            // For the request field to be populated, mountebank must be run with the --debug parameter
            // http://www.mbtest.org/docs/api/overview#get-imposter
            _retrievedImposter.Stubs.Count.Should().Be(1);
            _retrievedImposter.Stubs.ElementAt(0).Matches.Count.Should().Be(1);
        }

        private void CreateImposter()
        {
            var imposter = _client.CreateHttpImposter(ImposterPort);
            imposter.AddStub()
                .OnMethodEquals(Method.Get)
                .ReturnsStatus(HttpStatusCode.OK);
            _client.Submit(imposter);
        }

        private void MakeRequestToImposter()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri($"http://localhost:{ImposterPort}");
                client.GetAsync("").Wait();
            }
        }
    }
}