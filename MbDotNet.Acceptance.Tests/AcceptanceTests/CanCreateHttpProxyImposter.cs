using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        public override async Task Run()
        {
            await DeleteAllImposters().ConfigureAwait(false);
            await CreateSourceImposter().ConfigureAwait(false);
            await CreateProxyImposter().ConfigureAwait(false);
            await MakeRequestToImposter().ConfigureAwait(false);
            await GetSourceImposter().ConfigureAwait(false);
            VerifyRequestProxiedToSource();
            await DeleteAllImposters().ConfigureAwait(false);
        }

        private async Task GetSourceImposter()
        {
            _retrievedImposter = await _client.GetHttpImposterAsync(SourceImposterPort).ConfigureAwait(false);
        }

        private async Task DeleteAllImposters()
        {
            await _client.DeleteAllImpostersAsync().ConfigureAwait(false);
        }

        private void VerifyRequestProxiedToSource()
        {
            _retrievedImposter.Should().NotBeNull("Expected imposter to have been retrieved");
            _retrievedImposter.NumberOfRequests.Should().Be(1);
        }

        private async Task CreateSourceImposter()
        {
            var imposter = _client.CreateHttpImposter(SourceImposterPort);
            imposter.AddStub().ReturnsStatus(HttpStatusCode.OK);
            await _client.SubmitAsync(imposter).ConfigureAwait(false);
        }

        private async Task CreateProxyImposter()
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

            await _client.SubmitAsync(proxyImposter).ConfigureAwait(false);
        }

        private async Task MakeRequestToImposter()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri($"http://localhost:{ProxyImposterPort}");
                await client.GetAsync("test?param=value").ConfigureAwait(false);
            }
        }
    }
}
