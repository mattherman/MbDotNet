using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using MbDotNet.Enums;
using MbDotNet.Models.Imposters;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;

namespace MbDotNet.Acceptance.Tests.AcceptanceTests
{
    internal class CanCreateHttpProxyImposter : AcceptanceTest
    {
        private const int SourceImposterPort = 6000;
        private const int ProxyImposterPort = 6001;
        private RetrievedHttpImposter _retrievedImposter;

        public override void Run()
        {
            DeleteAllImposters();
            CreateSourceImposter();
            CreateProxyImposter();
            MakeRequestToImposter();
            GetSourceImposter();
            VerifyRequestProxiedToSource();
            DeleteAllImposters();
        }

        private void GetSourceImposter()
        {
            _retrievedImposter = _client.GetHttpImposterAsync(SourceImposterPort);
        }

        private void DeleteAllImposters()
        {
            _client.DeleteAllImpostersAsync();
        }

        private void VerifyRequestProxiedToSource()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
            _retrievedImposter.NumberOfRequests.Should().Be(1);
        }

        private void CreateSourceImposter()
        {
            var imposter = _client.CreateHttpImposter(SourceImposterPort);
            imposter.AddStub().ReturnsStatus(HttpStatusCode.OK);
            _client.SubmitAsync(imposter);
        }

        private void CreateProxyImposter()
        {
            var proxyImposter = _client.CreateHttpImposter(ProxyImposterPort);

            var predicateGenerators = new List<MatchesPredicate<HttpBooleanPredicateFields>>
            {
                new MatchesPredicate<HttpBooleanPredicateFields>(new HttpBooleanPredicateFields
                {
                    QueryParameters = true
                })
            };

            proxyImposter.AddStub().ReturnsProxy(
                new System.Uri($"http://localhost:{SourceImposterPort}"), 
                ProxyMode.ProxyOnce, predicateGenerators);

            _client.SubmitAsync(proxyImposter);
        }

        private void MakeRequestToImposter()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri($"http://localhost:{ProxyImposterPort}");
                client.GetAsync("test?param=value").Wait();
            }
        }
    }
}
